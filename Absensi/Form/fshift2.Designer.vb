<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fshift2
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tunit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tperiod = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tunit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tperiod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(120, 22)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        '    Me.tnama.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tnama.Size = New System.Drawing.Size(215, 20)
        Me.tnama.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(80, 25)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Nama :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(49, 53)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Unit Periode :"
        '
        'tunit
        '
        Me.tunit.Enabled = False
        Me.tunit.Location = New System.Drawing.Point(120, 48)
        Me.tunit.Name = "tunit"
        Me.tunit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tunit.Properties.Items.AddRange(New Object() {"MINGGU", "BULAN"})
        '  Me.tunit.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tunit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.tunit.Size = New System.Drawing.Size(215, 20)
        Me.tunit.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(71, 77)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Periode :"
        '
        'tperiod
        '
        Me.tperiod.Location = New System.Drawing.Point(120, 74)
        Me.tperiod.Name = "tperiod"
        Me.tperiod.Properties.Appearance.Options.UseTextOptions = True
        Me.tperiod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.tperiod.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tperiod.Properties.DisplayFormat.FormatString = "n0"
        Me.tperiod.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tperiod.Properties.EditFormat.FormatString = "n0"
        Me.tperiod.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tperiod.Properties.Mask.EditMask = "n0"
        Me.tperiod.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tperiod.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tperiod.Size = New System.Drawing.Size(88, 20)
        Me.tperiod.TabIndex = 2
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(66, 103)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl4.TabIndex = 8
        Me.LabelControl4.Text = "Tgl Mulai :"
        Me.LabelControl4.Visible = False
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(120, 100)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        ' Me.ttgl.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ttgl.Size = New System.Drawing.Size(215, 20)
        Me.ttgl.TabIndex = 3
        Me.ttgl.Visible = False
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(260, 149)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(75, 25)
        Me.btselesai.TabIndex = 5
        Me.btselesai.Text = "Se&lesai"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(179, 149)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 4
        Me.btsimpan.Text = "&Simpan"
        '
        'fshift2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 189)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tperiod)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tunit)
        Me.Controls.Add(Me.ttgl)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fshift2"
        Me.Text = "Shift Karyawan"
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tunit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tperiod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tunit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tperiod As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
