using Android.App;
using Android.Runtime;
using System.Reflection;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using XFNoSoftKeyboadEntryControl;
using XFNoSoftKeyboadEntryControl.Droid;

[assembly: Dependency(typeof (NativeHelper))]
[assembly: ResourceDesigner("Shelf.Droid.Resource", IsApplication = true)]
[assembly: AssemblyTitle("Shelf.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Shelf.Android")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: UsesPermission("android.permission.INTERNET")]
[assembly: UsesPermission("android.permission.WRITE_EXTERNAL_STORAGE")]
[assembly: ExportRenderer(typeof (SoftkeyboardDisabledEntry), typeof (SoftkeyboardDisabledEntryRenderer))]
[assembly: AssemblyVersion("1.0.0.0")]
