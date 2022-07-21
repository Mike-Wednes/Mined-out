using Core;

namespace WinFormsUI.Layers
{
    internal static class ComboBoxesDictionary
    {
        internal static Dictionary<string, Type> type = new Dictionary<string, Type>()
        {
            {"Border", typeof(BorderCell)},
            {"Mine", typeof(MineCell)},
            {"Space", typeof(BasicSpaceCell)},
            {"Finish", typeof(FinishSpaceCell)},
            {"Player", typeof(PlayerCell)},
        };
    }
}