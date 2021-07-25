using System.Windows.Forms;

namespace VanityMonKeyGenerator
{
    public partial class BetterCheckedListBox : CheckedListBox
    {
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            DrawItemState drawItemState = e.State;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                drawItemState &= ~DrawItemState.Selected;
            }
            DrawItemEventArgs de = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds,
                e.Index, drawItemState, ForeColor, BackColor);
            base.OnDrawItem(de);
        }
    }
}
