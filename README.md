# AndroidViewGenerator (Archived)

> Project moved to [ViewBindingsGenerator](https://github.com/panoukos41/ViewBindingsGenerator)

[![Release](https://github.com/panoukos41/AndroidViewGenerator/actions/workflows/release.yaml/badge.svg)](https://github.com/panoukos41/AndroidViewGenerator/actions/workflows/release.yaml)
[![NuGet](https://buildstats.info/nuget/P41.AndroidViewGenerator?includePreReleases=true)](https://www.nuget.org/packages/P41.AndroidViewGenerator)
[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/panoukos41/AndroidViewGenerator/blob/main/LICENSE.md)

A project that generates a partial class (view) for Activities/Fragments that will contain properties with the same name as their `android:id` in the xml view file.

## Getting Started

To get started make your activity or fragment partial and add the `[GenerateView("view_file.xml")]`

### Activity

```csharp
[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
[GenerateView("activity_main.xml")]
public partial class MainActivity : AppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_main);
    }
}
```

### Fragment

```csharp
[GenerateView("fragment_main.xml")]
public partial class MainFragment : Fragment
{
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        return inflater.Inflate(Resource.Layout.fragment_main, container, false)!;
    }
}
```

## Build

Install [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/preview) and [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

Clone the project and open the solution then you just build the whole solution or project.

## Contribute

Contributions are welcome and appreciated, before you create a pull request please open a [GitHub Issue](https://github.com/panoukos41/AndroidViewGenerator/issues/new) to discuss what needs changing and or fixing if the issue doesn't exist!
