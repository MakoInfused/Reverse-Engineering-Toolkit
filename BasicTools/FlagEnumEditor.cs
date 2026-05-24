using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace BasicTools
{
    public static class FlagConstants
    {
        public const byte None = 0;
        public const byte Flag1 = 1 << 0;
        public const byte Flag2 = 1 << 1;
        public const byte Flag3 = 1 << 2;
        public const byte Flag4 = 1 << 3;
        public const byte Flag5 = 1 << 4;
        public const byte Flag6 = 1 << 5;
        public const byte Flag7 = 1 << 6;
        public const byte Flag8 = 1 << 7;
        public const ushort Flag9 = 1 << 8;
        public const ushort Flag10 = 1 << 9;
        public const ushort Flag11 = 1 << 10;
        public const ushort Flag12 = 1 << 11;
        public const ushort Flag13 = 1 << 12;
        public const ushort Flag14 = 1 << 13;
        public const ushort Flag15 = 1 << 14;
        public const ushort Flag16 = 1 << 15;
        public const uint Flag17 = 1 << 16;
        public const uint Flag18 = 1 << 17;
        public const uint Flag19 = 1 << 18;
        public const uint Flag20 = 1 << 19;
        public const uint Flag21 = 1 << 20;
        public const uint Flag22 = 1 << 21;
        public const uint Flag23 = 1 << 22;
        public const uint Flag24 = 1 << 23;
        public const uint Flag25 = 1 << 24;
        public const uint Flag26 = 1 << 25;
        public const uint Flag27 = 1 << 26;
        public const uint Flag28 = 1 << 27;
        public const uint Flag29 = 1 << 28;
        public const uint Flag30 = 1 << 29;
        public const uint Flag31 = 1 << 30;
        public const int Flag32 = 1 << 31;
        public const int Allx86 = int.MaxValue;
        public const ulong Flag33 = 1 << 32;
        public const ulong Flag34 = 1 << 33;
        public const ulong Flag35 = 1 << 34;
        public const ulong Flag36 = 1 << 35;
        public const ulong Flag37 = 1 << 36;
        public const ulong Flag38 = 1 << 37;
        public const ulong Flag39 = 1 << 38;
        public const ulong Flag40 = 1 << 39;
        public const ulong Flag41 = 1 << 40;
        public const ulong Flag42 = 1 << 41;
        public const ulong Flag43 = 1 << 42;
        public const ulong Flag44 = 1 << 43;
        public const ulong Flag45 = 1 << 44;
        public const ulong Flag46 = 1 << 45;
        public const ulong Flag47 = 1 << 46;
        public const ulong Flag48 = 1 << 47;
        public const ulong Flag49 = 1 << 48;
        public const ulong Flag50 = 1 << 49;
        public const ulong Flag51 = 1 << 50;
        public const ulong Flag52 = 1 << 51;
        public const ulong Flag53 = 1 << 52;
        public const ulong Flag54 = 1 << 53;
        public const ulong Flag55 = 1 << 54;
        public const ulong Flag56 = 1 << 55;
        public const ulong Flag57 = 1 << 56;
        public const ulong Flag58 = 1 << 57;
        public const ulong Flag59 = 1 << 58;
        public const ulong Flag60 = 1 << 59;
        public const ulong Flag61 = 1 << 60;
        public const ulong Flag62 = 1 << 61;
        public const ulong Flag63 = 1 << 62;
        public const long Flag64 = 1 << 63;
        public const long All = long.MaxValue;
    }

    public class FlagCheckedListBox : CheckedListBox
	{
		private Container components = null;

		public FlagCheckedListBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		private void InitializeComponent()
		{
			// 
			// FlaggedCheckedListBox
			// 
			this.CheckOnClick = true;

		}
		#endregion

        // Adds an integer value and its associated description
		public FlagCheckedListBoxItem Add(int v,string c)
		{
			FlagCheckedListBoxItem item = new FlagCheckedListBoxItem(v,c);
			Items.Add(item);
			return item;
		}

		public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
		{
			Items.Add(item);
			return item;
		}

        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            base.OnItemCheck(e);

            if (isUpdatingCheckStates)
                return;

            // Get the checked/unchecked item
            FlagCheckedListBoxItem item = Items[e.Index] as FlagCheckedListBoxItem;
            // Update other items
            UpdateCheckedItems(item, e.NewValue);
        }

        // Checks/Unchecks items depending on the give bitvalue
		protected void UpdateCheckedItems(int value)
		{

			isUpdatingCheckStates = true;

            // Iterate over all items
			for(int i=0;i<Items.Count;i++)
			{
				FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

				if(item.value==0)
				{
					SetItemChecked(i,value==0);
				}
				else
				{

					// If the bit for the current item is on in the bitvalue, check it
					if( (item.value & value)== item.value && item.value!=0)
						SetItemChecked(i,true);
						// Otherwise uncheck it
					else
						SetItemChecked(i,false);
				}
			}

			isUpdatingCheckStates = false;

		}

        // Updates items in the checklistbox
        // composite = The item that was checked/unchecked
        // cs = The check state of that item
		protected void UpdateCheckedItems(FlagCheckedListBoxItem composite,CheckState cs)
		{

            // If the value of the item is 0, call directly.
			if(composite.value==0)
				UpdateCheckedItems(0);


            // Get the total value of all checked items
			int sum = 0;
			for(int i=0;i<Items.Count;i++)
			{
				FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                // If item is checked, add its value to the sum.
				if(GetItemChecked(i))
					sum |= item.value;
			}

            // If the item has been unchecked, remove its bits from the sum
			if(cs==CheckState.Unchecked)
				sum = sum & (~composite.value);
            // If the item has been checked, combine its bits with the sum
			else
				sum |= composite.value;

            // Update all items in the checklistbox based on the final bit value
			UpdateCheckedItems(sum);

		}

		private bool isUpdatingCheckStates = false;

        // Gets the current bit value corresponding to all checked items
		public int GetCurrentValue()
		{
			int sum = 0;

			for(int i=0;i<Items.Count;i++)
			{
				FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

				if( GetItemChecked(i))
					sum |= item.value;
			}

			return sum;
		}

		Type enumType;
		Enum enumValue;

		// Adds items to the checklistbox based on the members of the enum
		private void FillEnumMembers()
		{
			foreach ( string name in Enum.GetNames(enumType))
			{
				object val = Enum.Parse(enumType,name);
				int intVal = (int)Convert.ChangeType(val, typeof(int));

				Add(intVal,name);
			}
		}

		// Checks/unchecks items based on the current value of the enum variable
		private void ApplyEnumValue()
		{
			int intVal = (int)Convert.ChangeType(enumValue, typeof(int));
			UpdateCheckedItems(intVal);

		}

		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
		public Enum EnumValue
		{
			get
			{
				object e = Enum.ToObject(enumType,GetCurrentValue());
				return (Enum)e;
			}
			set
			{
                
				Items.Clear();
				enumValue = value; // Store the current enum value
				enumType = value.GetType(); // Store enum type
				FillEnumMembers(); // Add items for enum members
				ApplyEnumValue(); // Check/uncheck items depending on enum value

			}
		}


	}

    // Represents an item in the checklistbox
    public class FlagCheckedListBoxItem
    {
        public FlagCheckedListBoxItem(int v, string c)
        {
            value = v;
            caption = c;
        }

        public override string ToString()
        {
            return caption;
        }

        // Returns true if the value corresponds to a single bit being set
        public bool IsFlag
        {
            get
            {
                return ((value & (value - 1)) == 0);
            }
        }

        // Returns true if this value is a member of the composite bit value
        public bool IsMemberFlag(FlagCheckedListBoxItem composite)
        {
            return (IsFlag && ((value & composite.value) == value));
        }

        public int value;
        public string caption;
    }


    // UITypeEditor for flag enums
	public class FlagEnumUIEditor : UITypeEditor
	{
        // The checklistbox
		private FlagCheckedListBox flagEnumCB;

		public FlagEnumUIEditor()
		{
			flagEnumCB = new FlagCheckedListBox();
			flagEnumCB.BorderStyle = BorderStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if (context != null
				&& context.Instance != null
				&& provider != null) 
			{

				IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{					

					Enum e = (Enum) Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
					flagEnumCB.EnumValue = e;
					edSvc.DropDownControl(flagEnumCB);
					return flagEnumCB.EnumValue;

				}
			}
			return null;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			return UITypeEditorEditStyle.DropDown;			
		}

	}

}
