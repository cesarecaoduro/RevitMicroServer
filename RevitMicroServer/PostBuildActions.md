## Post Build actions
```
if exist "$(AppData)\Autodesk\REVIT\Addins\2020" copy "$(ProjectDir)*.addin" "$(AppData)\Autodesk\REVIT\Addins\2020"
if exist "$(AppData)\Autodesk\REVIT\Addins\2020" copy "$(ProjectDir)$(OutputPath)*.dll" "$(AppData)\Autodesk\REVIT\Addins\2020"
```