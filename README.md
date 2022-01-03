# Timesheet Marker Tool

Say you run a fancy time tracker like [ManicTime](https://manictime.com), [ActivityWatch](https://activitywatch.net/), ... which can track what software you are running and decide what you spent your time on that day. Tracking window titles is all fine and good, but as soon as you take one step away from the keyboard or are answering a question from a random visitor you lose context: the time tracker will just show whatever was last open on your screen.

This repo contains a simple WinForms application which asks for an activity description. The app will then update its window title so that the time tracking application can use that context. Optionally the context can be passed from command-line arguments, which could be useful if one wants to make a shortcut for common scenarios.

When done, the little helper window is easy to dismiss: hit the OS close button or activate the app's Close button by mouse click, pressing Enter or pressing Escape.


## Building

- Install the .NET 6.0 SDK.
- Run `dotnet publish`
- Grab the single 64-bits Windows exe from the publish folder. Any pdb files can be ignored.

## Use

Place the exe wherever you want, as long as it sits in a directory which can be written to. (Future versions of the tool might want to write a little config file to remember the last input, in case the app is accidentally closed).

The window title currently uses the following convention:

```
TimesheetMarkerTool - <ActivityType> @@ <Activity description> @@
```

When passing input, there are 5 kinds of formats:

- `ActivityType @ Activity`
- `P Activity description` -> shortcut for private stuff, ActivityType set to `Privé`
- `T Activity description` -> shortcut for `Temporary` activity, needs to be revisited later
- `F Activity description` -> Stuff which is just filler and doesn't need to be revisited, uses `Fill` activity type.
- `Activity description` -> Activity type will be set to `Temporary`, needs to be revisited.

### ManicTime Integration

For use with ManicTime you would then need to create a file `c:\Users\<User>\AppData\Local\Finkit\ManicTime\Plugins\Storage\ManicTime.DocumentTracker.CustomTitle\Content\CustomTitle.txt`. (You can copy information about the format from `c:\Program Files (x86)\ManicTime\Plugins\Packages\ManicTime.DocumentTracker.CustomTitle\Lib\CustomTitle.txt`)

Add a line like this, where you need to replace `<TAB>` with a literal Tab character:
```
timesheetmarkertool<TAB>[^-]+- ([^@]*) @@.*<TAB>.*@@ ([^@]*) @@$
```

After restarting ManicTime you will see activity types and descriptions in the Document timeline.

Its recommended to setup Autotags on the Applications timeline. Select that timeline and filter on `#"^TimesheetMarkerTool - ([^-]+) @@ ([^@]*) @@$"`,
then click the big "down" arrow and pick "Add to Autotags". Then one can set tag to `{{1}}` and description to `{{2}}`.

It's recommended to setup an "Absorb" autotag for the input though (and paired only with the rule above), that way the input gets automatically
matched to the entered activity.
