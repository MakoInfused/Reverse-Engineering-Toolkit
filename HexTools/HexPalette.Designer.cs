namespace HexTools
{
    partial class HexPalette
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.ExportButton = new BasicTools.BasicControls.BasicButton();
            this.ImportButton = new BasicTools.BasicControls.BasicButton();
            this.HexColorSwatch16 = new HexTools.HexColorSwatch();
            this.BasicColorSelector = new BasicTools.BasicControls.BasicColorSelector();
            this.HexColorSwatch15 = new HexTools.HexColorSwatch();
            this.HexColorSwatch14 = new HexTools.HexColorSwatch();
            this.HexColorSwatch13 = new HexTools.HexColorSwatch();
            this.HexColorSwatch12 = new HexTools.HexColorSwatch();
            this.HexColorSwatch11 = new HexTools.HexColorSwatch();
            this.HexColorSwatch10 = new HexTools.HexColorSwatch();
            this.HexColorSwatch09 = new HexTools.HexColorSwatch();
            this.HexColorSwatch08 = new HexTools.HexColorSwatch();
            this.HexColorSwatch07 = new HexTools.HexColorSwatch();
            this.HexColorSwatch06 = new HexTools.HexColorSwatch();
            this.HexColorSwatch05 = new HexTools.HexColorSwatch();
            this.HexColorSwatch04 = new HexTools.HexColorSwatch();
            this.HexColorSwatch03 = new HexTools.HexColorSwatch();
            this.HexColorSwatch02 = new HexTools.HexColorSwatch();
            this.HexColorSwatch01 = new HexTools.HexColorSwatch();
            this.ImportFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.ExportButton);
            this.GroupBox.Controls.Add(this.ImportButton);
            this.GroupBox.Controls.Add(this.HexColorSwatch16);
            this.GroupBox.Controls.Add(this.HexColorSwatch15);
            this.GroupBox.Controls.Add(this.HexColorSwatch14);
            this.GroupBox.Controls.Add(this.HexColorSwatch13);
            this.GroupBox.Controls.Add(this.HexColorSwatch12);
            this.GroupBox.Controls.Add(this.HexColorSwatch11);
            this.GroupBox.Controls.Add(this.HexColorSwatch10);
            this.GroupBox.Controls.Add(this.HexColorSwatch09);
            this.GroupBox.Controls.Add(this.HexColorSwatch08);
            this.GroupBox.Controls.Add(this.HexColorSwatch07);
            this.GroupBox.Controls.Add(this.HexColorSwatch06);
            this.GroupBox.Controls.Add(this.HexColorSwatch05);
            this.GroupBox.Controls.Add(this.HexColorSwatch04);
            this.GroupBox.Controls.Add(this.HexColorSwatch03);
            this.GroupBox.Controls.Add(this.HexColorSwatch02);
            this.GroupBox.Controls.Add(this.HexColorSwatch01);
            this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox.Location = new System.Drawing.Point(0, 0);
            this.GroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox.Size = new System.Drawing.Size(377, 101);
            this.GroupBox.TabIndex = 19;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Palette";
            // 
            // ExportButton
            // 
            this.ExportButton.Image = global::HexTools.My.Resources.Resources.Export;
            this.ExportButton.Location = new System.Drawing.Point(333, 60);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(32, 32);
            this.ExportButton.TabIndex = 40;
            this.ToolTip.SetToolTip(this.ExportButton, "Export Palette");
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Image = global::HexTools.My.Resources.Resources.Import;
            this.ImportButton.Location = new System.Drawing.Point(333, 25);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(32, 32);
            this.ImportButton.TabIndex = 39;
            this.ToolTip.SetToolTip(this.ImportButton, "Import Palette");
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // HexColorSwatch16
            // 
            this.HexColorSwatch16.AutoScroll = null;
            this.HexColorSwatch16.AutoScrollMargin = null;
            this.HexColorSwatch16.AutoScrollMinSize = null;
            this.HexColorSwatch16.AutoSize = null;
            this.HexColorSwatch16.AutoSizeMode = null;
            this.HexColorSwatch16.BackgroundImage = null;
            this.HexColorSwatch16.BackgroundImageLayout = null;
            this.HexColorSwatch16.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch16.Editor = this.BasicColorSelector;
            this.HexColorSwatch16.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch16.HexOffset = "&H00001E";
            this.HexColorSwatch16.Location = new System.Drawing.Point(293, 62);
            this.HexColorSwatch16.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch16.MaxLength = 2;
            this.HexColorSwatch16.Name = "HexColorSwatch16";
            this.HexColorSwatch16.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch16.TabIndex = 38;
            // 
            // BasicColorSelector
            // 
            this.BasicColorSelector.ShortcutKeys = true;
            // 
            // HexColorSwatch15
            // 
            this.HexColorSwatch15.AutoScroll = null;
            this.HexColorSwatch15.AutoScrollMargin = null;
            this.HexColorSwatch15.AutoScrollMinSize = null;
            this.HexColorSwatch15.AutoSize = null;
            this.HexColorSwatch15.AutoSizeMode = null;
            this.HexColorSwatch15.BackgroundImage = null;
            this.HexColorSwatch15.BackgroundImageLayout = null;
            this.HexColorSwatch15.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch15.Editor = this.BasicColorSelector;
            this.HexColorSwatch15.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch15.HexOffset = "&H00001C";
            this.HexColorSwatch15.Location = new System.Drawing.Point(253, 62);
            this.HexColorSwatch15.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch15.MaxLength = 2;
            this.HexColorSwatch15.Name = "HexColorSwatch15";
            this.HexColorSwatch15.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch15.TabIndex = 37;
            // 
            // HexColorSwatch14
            // 
            this.HexColorSwatch14.AutoScroll = null;
            this.HexColorSwatch14.AutoScrollMargin = null;
            this.HexColorSwatch14.AutoScrollMinSize = null;
            this.HexColorSwatch14.AutoSize = null;
            this.HexColorSwatch14.AutoSizeMode = null;
            this.HexColorSwatch14.BackgroundImage = null;
            this.HexColorSwatch14.BackgroundImageLayout = null;
            this.HexColorSwatch14.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch14.Editor = this.BasicColorSelector;
            this.HexColorSwatch14.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch14.HexOffset = "&H00001A";
            this.HexColorSwatch14.Location = new System.Drawing.Point(213, 62);
            this.HexColorSwatch14.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch14.MaxLength = 2;
            this.HexColorSwatch14.Name = "HexColorSwatch14";
            this.HexColorSwatch14.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch14.TabIndex = 36;
            // 
            // HexColorSwatch13
            // 
            this.HexColorSwatch13.AutoScroll = null;
            this.HexColorSwatch13.AutoScrollMargin = null;
            this.HexColorSwatch13.AutoScrollMinSize = null;
            this.HexColorSwatch13.AutoSize = null;
            this.HexColorSwatch13.AutoSizeMode = null;
            this.HexColorSwatch13.BackgroundImage = null;
            this.HexColorSwatch13.BackgroundImageLayout = null;
            this.HexColorSwatch13.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch13.Editor = this.BasicColorSelector;
            this.HexColorSwatch13.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch13.HexOffset = "&H000018";
            this.HexColorSwatch13.Location = new System.Drawing.Point(173, 62);
            this.HexColorSwatch13.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch13.MaxLength = 2;
            this.HexColorSwatch13.Name = "HexColorSwatch13";
            this.HexColorSwatch13.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch13.TabIndex = 35;
            // 
            // HexColorSwatch12
            // 
            this.HexColorSwatch12.AutoScroll = null;
            this.HexColorSwatch12.AutoScrollMargin = null;
            this.HexColorSwatch12.AutoScrollMinSize = null;
            this.HexColorSwatch12.AutoSize = null;
            this.HexColorSwatch12.AutoSizeMode = null;
            this.HexColorSwatch12.BackgroundImage = null;
            this.HexColorSwatch12.BackgroundImageLayout = null;
            this.HexColorSwatch12.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch12.Editor = this.BasicColorSelector;
            this.HexColorSwatch12.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch12.HexOffset = "&H000016";
            this.HexColorSwatch12.Location = new System.Drawing.Point(133, 62);
            this.HexColorSwatch12.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch12.MaxLength = 2;
            this.HexColorSwatch12.Name = "HexColorSwatch12";
            this.HexColorSwatch12.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch12.TabIndex = 34;
            // 
            // HexColorSwatch11
            // 
            this.HexColorSwatch11.AutoScroll = null;
            this.HexColorSwatch11.AutoScrollMargin = null;
            this.HexColorSwatch11.AutoScrollMinSize = null;
            this.HexColorSwatch11.AutoSize = null;
            this.HexColorSwatch11.AutoSizeMode = null;
            this.HexColorSwatch11.BackgroundImage = null;
            this.HexColorSwatch11.BackgroundImageLayout = null;
            this.HexColorSwatch11.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch11.Editor = this.BasicColorSelector;
            this.HexColorSwatch11.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch11.HexOffset = "&H000014";
            this.HexColorSwatch11.Location = new System.Drawing.Point(93, 62);
            this.HexColorSwatch11.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch11.MaxLength = 2;
            this.HexColorSwatch11.Name = "HexColorSwatch11";
            this.HexColorSwatch11.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch11.TabIndex = 33;
            // 
            // HexColorSwatch10
            // 
            this.HexColorSwatch10.AutoScroll = null;
            this.HexColorSwatch10.AutoScrollMargin = null;
            this.HexColorSwatch10.AutoScrollMinSize = null;
            this.HexColorSwatch10.AutoSize = null;
            this.HexColorSwatch10.AutoSizeMode = null;
            this.HexColorSwatch10.BackgroundImage = null;
            this.HexColorSwatch10.BackgroundImageLayout = null;
            this.HexColorSwatch10.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch10.Editor = this.BasicColorSelector;
            this.HexColorSwatch10.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch10.HexOffset = "&H000012";
            this.HexColorSwatch10.Location = new System.Drawing.Point(53, 62);
            this.HexColorSwatch10.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch10.MaxLength = 2;
            this.HexColorSwatch10.Name = "HexColorSwatch10";
            this.HexColorSwatch10.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch10.TabIndex = 32;
            // 
            // HexColorSwatch09
            // 
            this.HexColorSwatch09.AutoScroll = null;
            this.HexColorSwatch09.AutoScrollMargin = null;
            this.HexColorSwatch09.AutoScrollMinSize = null;
            this.HexColorSwatch09.AutoSize = null;
            this.HexColorSwatch09.AutoSizeMode = null;
            this.HexColorSwatch09.BackgroundImage = null;
            this.HexColorSwatch09.BackgroundImageLayout = null;
            this.HexColorSwatch09.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch09.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch09.Editor = this.BasicColorSelector;
            this.HexColorSwatch09.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch09.HexOffset = "&H000010";
            this.HexColorSwatch09.Location = new System.Drawing.Point(13, 62);
            this.HexColorSwatch09.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch09.MaxLength = 2;
            this.HexColorSwatch09.Name = "HexColorSwatch09";
            this.HexColorSwatch09.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch09.TabIndex = 31;
            // 
            // HexColorSwatch08
            // 
            this.HexColorSwatch08.AutoScroll = null;
            this.HexColorSwatch08.AutoScrollMargin = null;
            this.HexColorSwatch08.AutoScrollMinSize = null;
            this.HexColorSwatch08.AutoSize = null;
            this.HexColorSwatch08.AutoSizeMode = null;
            this.HexColorSwatch08.BackgroundImage = null;
            this.HexColorSwatch08.BackgroundImageLayout = null;
            this.HexColorSwatch08.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch08.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch08.Editor = this.BasicColorSelector;
            this.HexColorSwatch08.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch08.HexOffset = "&H00000E";
            this.HexColorSwatch08.Location = new System.Drawing.Point(293, 25);
            this.HexColorSwatch08.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch08.MaxLength = 2;
            this.HexColorSwatch08.Name = "HexColorSwatch08";
            this.HexColorSwatch08.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch08.TabIndex = 23;
            // 
            // HexColorSwatch07
            // 
            this.HexColorSwatch07.AutoScroll = null;
            this.HexColorSwatch07.AutoScrollMargin = null;
            this.HexColorSwatch07.AutoScrollMinSize = null;
            this.HexColorSwatch07.AutoSize = null;
            this.HexColorSwatch07.AutoSizeMode = null;
            this.HexColorSwatch07.BackgroundImage = null;
            this.HexColorSwatch07.BackgroundImageLayout = null;
            this.HexColorSwatch07.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch07.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch07.Editor = this.BasicColorSelector;
            this.HexColorSwatch07.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch07.HexOffset = "&H00000C";
            this.HexColorSwatch07.Location = new System.Drawing.Point(253, 25);
            this.HexColorSwatch07.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch07.MaxLength = 2;
            this.HexColorSwatch07.Name = "HexColorSwatch07";
            this.HexColorSwatch07.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch07.TabIndex = 22;
            // 
            // HexColorSwatch06
            // 
            this.HexColorSwatch06.AutoScroll = null;
            this.HexColorSwatch06.AutoScrollMargin = null;
            this.HexColorSwatch06.AutoScrollMinSize = null;
            this.HexColorSwatch06.AutoSize = null;
            this.HexColorSwatch06.AutoSizeMode = null;
            this.HexColorSwatch06.BackgroundImage = null;
            this.HexColorSwatch06.BackgroundImageLayout = null;
            this.HexColorSwatch06.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch06.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch06.Editor = this.BasicColorSelector;
            this.HexColorSwatch06.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch06.HexOffset = "&H00000A";
            this.HexColorSwatch06.Location = new System.Drawing.Point(213, 25);
            this.HexColorSwatch06.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch06.MaxLength = 2;
            this.HexColorSwatch06.Name = "HexColorSwatch06";
            this.HexColorSwatch06.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch06.TabIndex = 21;
            // 
            // HexColorSwatch05
            // 
            this.HexColorSwatch05.AutoScroll = null;
            this.HexColorSwatch05.AutoScrollMargin = null;
            this.HexColorSwatch05.AutoScrollMinSize = null;
            this.HexColorSwatch05.AutoSize = null;
            this.HexColorSwatch05.AutoSizeMode = null;
            this.HexColorSwatch05.BackgroundImage = null;
            this.HexColorSwatch05.BackgroundImageLayout = null;
            this.HexColorSwatch05.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch05.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch05.Editor = this.BasicColorSelector;
            this.HexColorSwatch05.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch05.HexOffset = "&H000008";
            this.HexColorSwatch05.Location = new System.Drawing.Point(173, 25);
            this.HexColorSwatch05.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch05.MaxLength = 2;
            this.HexColorSwatch05.Name = "HexColorSwatch05";
            this.HexColorSwatch05.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch05.TabIndex = 20;
            // 
            // HexColorSwatch04
            // 
            this.HexColorSwatch04.AutoScroll = null;
            this.HexColorSwatch04.AutoScrollMargin = null;
            this.HexColorSwatch04.AutoScrollMinSize = null;
            this.HexColorSwatch04.AutoSize = null;
            this.HexColorSwatch04.AutoSizeMode = null;
            this.HexColorSwatch04.BackgroundImage = null;
            this.HexColorSwatch04.BackgroundImageLayout = null;
            this.HexColorSwatch04.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch04.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch04.Editor = this.BasicColorSelector;
            this.HexColorSwatch04.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch04.HexOffset = "&H000006";
            this.HexColorSwatch04.Location = new System.Drawing.Point(133, 25);
            this.HexColorSwatch04.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch04.MaxLength = 2;
            this.HexColorSwatch04.Name = "HexColorSwatch04";
            this.HexColorSwatch04.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch04.TabIndex = 18;
            // 
            // HexColorSwatch03
            // 
            this.HexColorSwatch03.AutoScroll = null;
            this.HexColorSwatch03.AutoScrollMargin = null;
            this.HexColorSwatch03.AutoScrollMinSize = null;
            this.HexColorSwatch03.AutoSize = null;
            this.HexColorSwatch03.AutoSizeMode = null;
            this.HexColorSwatch03.BackgroundImage = null;
            this.HexColorSwatch03.BackgroundImageLayout = null;
            this.HexColorSwatch03.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch03.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch03.Editor = this.BasicColorSelector;
            this.HexColorSwatch03.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch03.HexOffset = "&H000004";
            this.HexColorSwatch03.Location = new System.Drawing.Point(93, 25);
            this.HexColorSwatch03.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch03.MaxLength = 2;
            this.HexColorSwatch03.Name = "HexColorSwatch03";
            this.HexColorSwatch03.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch03.TabIndex = 19;
            // 
            // HexColorSwatch02
            // 
            this.HexColorSwatch02.AutoScroll = null;
            this.HexColorSwatch02.AutoScrollMargin = null;
            this.HexColorSwatch02.AutoScrollMinSize = null;
            this.HexColorSwatch02.AutoSize = null;
            this.HexColorSwatch02.AutoSizeMode = null;
            this.HexColorSwatch02.BackgroundImage = null;
            this.HexColorSwatch02.BackgroundImageLayout = null;
            this.HexColorSwatch02.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch02.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch02.Editor = this.BasicColorSelector;
            this.HexColorSwatch02.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch02.HexOffset = "&H000002";
            this.HexColorSwatch02.Location = new System.Drawing.Point(53, 25);
            this.HexColorSwatch02.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch02.MaxLength = 2;
            this.HexColorSwatch02.Name = "HexColorSwatch02";
            this.HexColorSwatch02.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch02.TabIndex = 18;
            // 
            // HexColorSwatch01
            // 
            this.HexColorSwatch01.AutoScroll = null;
            this.HexColorSwatch01.AutoScrollMargin = null;
            this.HexColorSwatch01.AutoScrollMinSize = null;
            this.HexColorSwatch01.AutoSize = null;
            this.HexColorSwatch01.AutoSizeMode = null;
            this.HexColorSwatch01.BackgroundImage = null;
            this.HexColorSwatch01.BackgroundImageLayout = null;
            this.HexColorSwatch01.BorderColor = System.Drawing.SystemColors.ControlText;
            this.HexColorSwatch01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HexColorSwatch01.Editor = this.BasicColorSelector;
            this.HexColorSwatch01.Endian = HexTools.HexEnumerations.EndianType.Little_Endian;
            this.HexColorSwatch01.Location = new System.Drawing.Point(13, 25);
            this.HexColorSwatch01.Margin = new System.Windows.Forms.Padding(5);
            this.HexColorSwatch01.MaxLength = 2;
            this.HexColorSwatch01.Name = "HexColorSwatch01";
            this.HexColorSwatch01.Size = new System.Drawing.Size(32, 30);
            this.HexColorSwatch01.TabIndex = 17;
            // 
            // ImportFileDialog
            // 
            this.ImportFileDialog.Title = "Import Palette";
            // 
            // ExportFileDialog
            // 
            this.ExportFileDialog.Title = "Export Palette";
            // 
            // HexPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBox);
            this.Name = "HexPalette";
            this.Size = new System.Drawing.Size(377, 101);
            this.GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox;
        internal HexColorSwatch HexColorSwatch16;
        internal HexColorSwatch HexColorSwatch15;
        internal HexColorSwatch HexColorSwatch14;
        internal HexColorSwatch HexColorSwatch13;
        internal HexColorSwatch HexColorSwatch12;
        internal HexColorSwatch HexColorSwatch11;
        internal HexColorSwatch HexColorSwatch10;
        internal HexColorSwatch HexColorSwatch09;
        internal HexColorSwatch HexColorSwatch08;
        internal HexColorSwatch HexColorSwatch07;
        internal HexColorSwatch HexColorSwatch06;
        internal HexColorSwatch HexColorSwatch05;
        internal HexColorSwatch HexColorSwatch04;
        internal HexColorSwatch HexColorSwatch03;
        internal HexColorSwatch HexColorSwatch02;
        internal HexColorSwatch HexColorSwatch01;
        internal BasicTools.BasicControls.BasicColorSelector BasicColorSelector;
        private BasicTools.BasicControls.BasicButton ImportButton;
        private BasicTools.BasicControls.BasicButton ExportButton;
        private System.Windows.Forms.OpenFileDialog ImportFileDialog;
        private System.Windows.Forms.SaveFileDialog ExportFileDialog;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}
