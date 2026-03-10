namespace FinTrack.Libraries.Utils
{
    public static class KeyboardFixBugs
    {
        public static void HideKeyboard()
        {
#if ANDROID
            if (Platform.CurrentActivity?.CurrentFocus != null)
            {
                var imm = (Android.Views.InputMethods.InputMethodManager?)
                    Platform.CurrentActivity.GetSystemService(
                        Android.Content.Context.InputMethodService);
                imm?.HideSoftInputFromWindow(
                    Platform.CurrentActivity.CurrentFocus.WindowToken, 0);
            }
#endif
        }
    }
}