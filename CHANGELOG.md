

This repo is a fork obtained from https://github.com/leocb/MaterialSkin.git
  - Revision: e14384f49f142cde712951824f6c461b57a0032b
  - Author: orapps44 <77468294+orapps44@users.noreply.github.com>
  - Date: 06/02/2022 21:50:11



build 2.3.1.4 [2022-02-20] 
-------------------------
  - Enhancements:
    - Added support for "UserControl" Containers
      - Previous versions only supported placing controls like MaterialDialog if the Parent Container was of a type that inherits from Winform (Form class), like MaterialForm or plain Winform. Adding MaterialDialog control to a custom UserControl would generate a runtime exception because MaterialDialog._formOverlay was expecting an "Onwer" of type "Form" which is not the case when using parent containers like UserControls.
      - Changed class "MaterialDialog" to expect a Parent-container of type "ContainerControl", Form and UserControl classes inherit from ContainControl class.
      - Affected files:
        - MaterialDialog.cs
        - MaterialSkinManager.cs

    - Improved design-mode experience of MaterialDrawer when combined with MaterialTabControl object. Now, during design-time, it's possible to view the drawer and the design impact on it when drawer's and tabcontrol's properties are changed by the user.

    - Improved performance of MaterialDrawer during initialization
      - Reduced the number of "Redraws" (Pain events) while the MaterialDrawer control is being initialized by its container.
        - Previously, during MaterialDrawer's initialization, every property set done by the container that would impact drawer's UI would generate a redraw event (paint event).
        - Now, MaterialDrawer control checks if its container has finish the initialization and if not ignores the redraw. When container invokes control's "InitLayout" method, meaning "end of control's initialization phase, future propertities changes will invoke a redraw action.




build 2.3.1.3 [2022-02-20] 
-------------------------
  - Code Cleansing:
    - moved all p/invoke declarations to a new static class "NativeWin" (NativeWin.cs)
      - there were a significant number of duplicated declarations on several controls
      - affected files:
        - NativeTextRenderer.cs
        - MouseWheelRedirector.cs
        - MaterialDialog.cs
        - MaterialForm.cs
        - MaterialMultiLineTextBox.cs
        - MaterialMultiLineTextBox2.cs
        - MaterialScrollBar.cs
        - MaterialSnackBar.cs
        - MaterialTextBox.cs
        - MaterialSkinManager.cs

    - moved several const declarations related to native Window Message IDs to new static class "NativeWin"
      - affected files:
        - MouseWheelRedirector.cs

    - moved all possible category's labels to a new static class "CategoryLabels" (Globals.cs) and replaced the Category Labels by the corresponding const string
      - affected files:
        - MaterialButton.cs
        - MaterialCheckBox.cs
        - MaterialComboBox.cs
        - MaterialDrawer.cs
        - MaterialExpansionPanel.cs
        - MaterialFloatingActionButton.cs
        - MaterialForm.cs
        - MaterialLabel.cs
        - MaterialListBox.cs
        - MaterialListView.cs
        - MaterialMaskedTextBox.cs
        - MaterialMultiLineTextBox.cs
        - MaterialMultiLineTextBox2.cs
        - MaterialRadioButton.cs
        - MaterialScrollBar.cs
        - MaterialSlider.cs
        - MaterialSnackBar.cs
        - MaterialSwitch.cs
        - MaterialTabSelector.cs
        - MaterialTextBox.cs
        - MaterialTextBox2.cs

    - moved several const declarations and enum declarations that were shared among files to a new file Globals.cs
        - MaterialDrawer.cs

  - Enhancement - compatibility with Msft CLI/C++:
    - enum value MouseState.OUT (IMaterialControl.cs) renamed to MouseState.OUT_
      - "OUT" keyword is a reserved word on CLI/C++.
