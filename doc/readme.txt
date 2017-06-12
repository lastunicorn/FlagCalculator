Flag Calculator v 1.1.0
=======================

It is a tool for building a value from combining flags from a .NET enum.
The application opens an enum from a .NET assembly and displays all its members. The user can select/unselect them, and the resulted value is displayed in the upper part of the window.


Features
--------
- Loads an enum from any .NET assembly. (It should be specified in app.config)
- Displays the list of enum's members along with their values.
- Lets user select/unselect flags.
- Calculates the value resulted by combining the selected flags and display it in base10 base 16 and base2.
- Copy/pastes the calculated value.


How to - load an enum
---------------------
The enum must be specified in app.config. (It cannot be loaded from the GUI):
1) Close the app.
2) Edit app.config and specify the full name of the enum in <appSettings>, the key "enumTypeName".
3) Start the app.


Future features
---------------
- Load the enum from the GUI instead of app.config.