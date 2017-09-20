# FlagCalculator v 1.3.1

This tool helps you build an integer by selecting the discrete flag values of a c# enum type.
The application opens an enum from a .NET assembly and displays all its members. The user can select/unselect them. The resulted value is displayed and can be copyed to be used where is needed.


Features
--------
- Can edit multiple values. Each one in its own tab.
- The enum types can be specified in app.config file or can be loaded from the GUI.
- The values are displayed in base10, base 16 or base2.
- Allows the user to edit the value by selecting/unselecting flags or by directly writing the integer value.
- Copy/pastes the calculated value in the selected base (base10 base 16 and base2).


How to - load an enum from app.config
-------------------------------------
1) Close the app.
2) Edit app.config and specify the full name of the enums in /configuration/flagCalculator/enumTypes list. Example found in the app.config file.
3) Start the app.


How to - load an enum from GUI
------------------------------
If no enum type is specified in the app.config, a button is displayed in the middle of the window to let the user select an enum type from an assembly.
1) Open a new tab: Ctrl+T
2) Click "Open Assembly..." button.
3) Select an assembly (dll or exe).
4) Select an enum type form the displayed list by double-clicking it.
