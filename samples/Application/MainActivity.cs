using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using P41.AndroidViewGenerator;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace Application
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    [GenerateView("activity_main.xml")]
    public partial class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            //var s = "com.google.android.material.progressindicator.CircularProgressIndicator";
            var s = "progressindicator";

            HelloText.Text = "Hello Generetor";
            ClickMe.Text = "Generated";

            //Spinner
            //AndroidX.AppCompat.Widget.AppCompatSpinner
            //Google.Android.Material.ProgressIndicator.CircularProgressIndicator;
        }
    }

    [GenerateView("fragment_main.xml")]
    public partial class MainFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_main, container, false)!;
        }
    }
}
