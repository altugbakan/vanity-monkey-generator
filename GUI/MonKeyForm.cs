using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using VanityMonKeyGenerator;

namespace GUI
{
    public partial class MonKeyForm : Form
    {
        private const int MaxRequestCount = 32;

        public MonKeyForm()
        {
            InitializeComponent();
            LoadDefaultMonKey();
        }

        private void MonKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            if (monKeySearcher.IsBusy)
            {
                monKeySearcher.CancelAsync();
            }
            while (monKeySearcher.IsBusy)
            {
                Application.DoEvents();
            }
        }

        private void GetRandomMonKeyButton_Click(object sender, EventArgs e)
        {
            MonKey monKey = new MonKey();
            monKeyPictureBox.ImageLocation = monKey.ImageUri("png", monKeyPictureBox.Width, false);
            addressTextBox.Text = monKey.Address;
            seedTextBox.Text = monKey.Seed;
        }

        private void FindSpecificMonKeyButton_Click(object sender, EventArgs e)
        {
            if (findSpecificMonKeyButton.Text == "Find Specific MonKey")
            {
                while (Properties.Settings.Default.SavedAccessories == null)
                {
                    DialogResult dialogResult = MessageBox.Show("Seems like you haven't yet created a" +
                        " specific MonKey. Want to create one?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SimpleSettings settingsForm = new SimpleSettings();
                        settingsForm.ShowDialog();
                    }
                    else
                    {
                        GetRandomMonKeyButton_Click(null, null);
                        return;
                    }
                }
                monKeySearcher.RunWorkerAsync();
                findSpecificMonKeyButton.Text = "Cancel";
            }
            else
            {
                monKeySearcher.CancelAsync();
            }
        }

        private void LoadDefaultMonKey()
        {
            Drawing.DrawMonKey(new List<string>() { "Mouths-SmileNormal" }, monKeyPictureBox);
        }
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SimpleMode)
            {
                SimpleSettings simpleSettings = new SimpleSettings();
                simpleSettings.ShowDialog();
            }
            else
            {
                ExpertSettings expertSettings = new ExpertSettings();
                expertSettings.ShowDialog();
            }
        }

        private void MonKeySearcher_DoWork(object sender, DoWorkEventArgs e)
        {
            ServicePointManager.DefaultConnectionLimit = MaxRequestCount * 2;
            HttpClient client = new HttpClient();
            List<string> requestedAccessories = Properties.Settings.Default.SavedAccessories
                .Cast<string>().ToList();
            List<Task<List<MonKey>>> tasks = new List<Task<List<MonKey>>>();
            ulong expectation = Accessories.GetMonKeyRarity(requestedAccessories);
            ulong iterations = 0;

            for (int i = 0; i < MaxRequestCount; i++)
            {
                tasks.Add(GetMonKeysAsync(client));
            }

            while (!monKeySearcher.CancellationPending)
            {
                Task.WaitAny(tasks.ToArray());

                for (int i = 0; i < tasks.Count; i++)
                {
                    if (monKeySearcher.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        if (tasks[i].IsCompleted)
                        {
                            ulong localIteration = 0;
                            foreach (MonKey monKey in tasks[i].Result)
                            {
                                if (Accessories.AccessoriesMatching(requestedAccessories, monKey.Accessories))
                                {
                                    e.Result = new Result(monKey, iterations + localIteration);
                                    return;
                                }
                                localIteration++;
                            }
                            tasks.RemoveAt(i);
                            tasks.Add(GetMonKeysAsync(client));
                            iterations += (ulong)Properties.Settings.Default.MonKeyRequestAmount;
                            monKeySearcher.ReportProgress(0, new ProgressResult(expectation, iterations));
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Connection error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void MonKeySearcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressResult result = (ProgressResult)e.UserState;
            searchedLabel.Text = $"Searched {result.Iterations:#,#} MonKeys. " +
                $"Estimated: {result.Expectation:#,#}";
        }

        private void MonKeySearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && (e.Result != null))
            {
                Result result = (Result)e.Result;
                monKeyPictureBox.ImageLocation = result.MonKey.ImageUri("png", monKeyPictureBox.Width, false);
                addressTextBox.Text = result.MonKey.Address;
                seedTextBox.Text = result.MonKey.Seed;
                searchedLabel.Text = $"Found MonKey after {result.Iterations:#,#} MonKeys.";
            }
            else
            {
                searchedLabel.Text = "";
            }
            findSpecificMonKeyButton.Text = "Find Specific MonKey";
        }

        private async Task<List<MonKey>> GetMonKeysAsync(HttpClient client)
        {
            Dictionary<string, MonKey> monKeyDictionary = new Dictionary<string, MonKey>();

            for (int i = 0; i < Properties.Settings.Default.MonKeyRequestAmount; i++)
            {
                MonKey monKey = new MonKey();
                monKeyDictionary.Add(monKey.Address, monKey);
            }
            var content = new StringContent("{\"addresses\":" + JsonSerializer.Serialize(monKeyDictionary.Keys) + "}");
            var response = await client.PostAsync("http://monkey.banano.cc/api/v1/monkey/dtl", content);
            var results = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(await response.Content.ReadAsStringAsync());
            foreach (var result in results)
            {
                monKeyDictionary[result.Key].Accessories = Accessories.ObtainedAccessories(result.Value);
            }

            return monKeyDictionary.Values.ToList();
        }

        public class Result
        {
            public MonKey MonKey;
            public ulong Iterations;

            public Result(MonKey monKey, ulong iterations)
            {
                MonKey = monKey;
                Iterations = iterations;
            }
        }

        public class ProgressResult
        {
            public ulong Expectation;
            public ulong Iterations;

            public ProgressResult(ulong expectation, ulong iterations)
            {
                Expectation = expectation;
                Iterations = iterations;
            }
        }
    }
}
