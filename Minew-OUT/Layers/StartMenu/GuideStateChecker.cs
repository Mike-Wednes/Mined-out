namespace WinFormsUI.Layers
{
    internal static class GuideStateChecker
    {
        public static bool IsDone()
        {
            using (StreamReader sr = new StreamReader(@"UIEventsSettings\guideDone.txt"))
            {
                if (sr.ReadToEnd() == "1")
                {
                    return true;
                }
            }
            return false;
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
