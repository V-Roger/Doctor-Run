using System;

namespace Doctor_Run
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DoctorRun game = new DoctorRun())
            {
                game.Run();
            }
        }
    }
#endif
}

