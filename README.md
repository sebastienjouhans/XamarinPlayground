# XamarinPlayground

Testing Xamarin features for Android and iOS with MVVMCross

At the time of mvvmcross nuget package had a bug and did not add the MvxBindingAttributes.xml in the Resources/Values folder which caused the Android app not to compile. http://stackoverflow.com/questions/34821295/no-resource-identifier-found-for-attribute-mvxbind-in-package-in-xamarin-andro/34902729#34902729

== ANDROID ==
  * mvvmcross implementation
  * simple button click bind to a command in the viewmodel
  * viewmodle updating the view
  * viewmodel navigation with parameters
  * viewmodel life cycle with init, start, save state, reload state
  * comm service
  * list view with click command
  * custom list view items
  * progress bar with visibilty converter (using mvvmcross visibility plugin)
  * image loading in the item list view (using mvvmcross file and download cache plugin)
