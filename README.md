# XamarinPlayground

Testing Xamarin features for Android and iOS with MVVMCross

At the time of mvvmcross nuget package had a bug and did not add the MvxBindingAttributes.xml in the Resources/Values folder which caused the Android app not to compile. http://stackoverflow.com/questions/34821295/no-resource-identifier-found-for-attribute-mvxbind-in-package-in-xamarin-andro/34902729#34902729

  * mvvmcross implementation
  * simple button click bind to a command in the viewmodel
  * viewmodle updating the view
  * viewmodel navigation with parameters
  * viewmodel life cycle with init, start, save state, reload state
  * 
