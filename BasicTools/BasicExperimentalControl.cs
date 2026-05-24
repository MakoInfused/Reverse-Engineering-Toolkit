using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace BasicTools
{

    public partial class BasicExperimentalControl
    {

        public BasicExperimentalControl()
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            SystemFormEvent.OnLifeCycleInitialized += (_, __) => SyncState();
            SystemFormEvent.OnLifeCyclePreferencesSaved += ConfirmationRefresh;
        }

        private void SyncState()
        {
            if (Control != null)
            {
                Control.Enabled = ShowConfirmation ? CheckBox.Checked : !CheckBox.Checked;
                CheckBox.Text = !Control.Enabled ? "Enable Experimental" : "Disable Experimental";
            }
        }

        private void ConfirmationRefresh(object Sender, SystemFormLifeCycle Args)
        {
            dynamic target = Sender;
            // TODO: I should use an interface here to determine if SupressUnsafeWarnings is true or not.
            ShowConfirmation = !DynamicUtility.SafeAccess(() => target.SupressUnsafeWarnings, false);
            SyncState();
        }

        private Control _Control = null;
        [Category("Behavior")]
        [Description("Marks the associated control as experimental, requiring extra user input to allow modification.")]
        [DefaultValue(default(string))]
        public Control Control
        {
            get
            {
                return _Control;
            }
            set
            {
                if (!ReferenceEquals(_Control, value))
                {
                    _Control = value;
                    SyncState();
                }
            }
        }

        [Category("Behavior")]
        [Description("Marks the associated control as experimental, requiring extra user input to allow modification.")]
        [DefaultValue(true)]
        public bool ShowConfirmation { get; set; }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool Checked = CheckBox.Checked;
            CheckBox.Checked = false;
            SystemFormEvent.ConfirmDialog(this, Checked & ShowConfirmation, $"enable the {Control.Name} control", (x, y) =>
                {
                    CheckBox.Checked = Checked;
                    SyncState();
                }, "This Control is experimental and potentially unsafe. ");
        }
    }
}