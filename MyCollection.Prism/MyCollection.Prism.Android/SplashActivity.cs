using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace MyCollection.Prism.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash",
              MainLauncher = true,
              NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            System.Threading.Thread.Sleep(1);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
