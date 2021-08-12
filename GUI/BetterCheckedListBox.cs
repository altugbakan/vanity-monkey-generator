using System.Windows.Forms;

namespace GUI
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

        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            if (CheckedItems.Count == 1 && ice.NewValue == CheckState.Unchecked)
            {
                ice.NewValue = ice.CurrentValue;
            }
            base.OnItemCheck(ice);
        }

        public void SetAllItemsChecked(bool value)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                SetItemChecked(i, value);
            }
        }

        public void SetAllItemsChecked(bool value, int exceptionIndex)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == exceptionIndex)
                {
                    SetItemChecked(i, !value);
                }
                else
                {
                    SetItemChecked(i, value);
                }
            }
        }
    }
}
