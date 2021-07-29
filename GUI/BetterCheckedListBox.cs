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
    }
}
