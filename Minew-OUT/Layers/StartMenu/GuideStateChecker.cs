namespace WinFormsUI.Layers
{
    internal static class GuideStateChecker
    {
        public static bool IsDone()
        {
            using (StreamReader sr = new StreamReader(@"UIEventsSettings\guideDone.txt"))
            {
                return sr.ReadToEnd() == "1";
            }
        }

        public static void WriteDone()
        {
            using (StreamWriter sw = new StreamWriter(@"UIEventsSettings\guideDone.txt", false))
            {
                sw.Write("1");
            }
        }
    }
}
