

This repo is a fork obtained from https://github.com/leocb/MaterialSkin.git
  - Revision: e14384f49f142cde712951824f6c461b57a0032b
  - Author: orapps44 <77468294+orapps44@users.noreply.github.com>
  - Date: 06/02/2022 21:50:11



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
