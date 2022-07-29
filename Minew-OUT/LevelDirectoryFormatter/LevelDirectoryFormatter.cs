using Logic;

namespace WinFormsUI
{
    public static class LevelDirectoryFormatter
    {
        public static string Path()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Levels/";
                openFileDialog.Filter = "json files (*.json)| *.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return "";
        }

        public static void Save(Level level)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "Levels/";
                saveFileDialog.Filter = "json files (*.json)| *.json";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.FileName = level.AdaptNameToPath();

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    level.Save(saveFileDialog.FileName);
                }
            }
        }
    }
}
