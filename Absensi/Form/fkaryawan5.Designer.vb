<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkaryawan5
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
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.cbjamkerja = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.thit = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbjamkerja.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.thit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(237, 144)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(61, 23)
        Me.btselesai.TabIndex = 5
        Me.btselesai.Text = "Selesai"
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(170, 144)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(61, 23)
        Me.btok.TabIndex = 4
        Me.btok.Text = "Ok"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(36, 15)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Tgl Mulai :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(90, 12)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        '    Me.ttgl.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Size = New System.Drawing.Size(118, 20)
        Me.ttgl.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(36, 41)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl3.TabIndex = 7
        Me.LabelControl3.Text = "Tgl Akhir :"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(90, 38)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        '   Me.ttgl2.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Size = New System.Drawing.Size(118, 20)
        Me.ttgl2.TabIndex = 1
        '
        'cbjamkerja
        '
        Me.cbjamkerja.Location = New System.Drawing.Point(90, 64)
        Me.cbjamkerja.Name = "cbjamkerja"
        Me.cbjamkerja.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbjamkerja.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbjamkerja.Properties.DisplayMember = "nama"
        Me.cbjamkerja.Properties.NullText = ""
        Me.cbjamkerja.Properties.PopupSizeable = False
        Me.cbjamkerja.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbjamkerja.Properties.ValueMember = "kode"
        Me.cbjamkerja.Size = New System.Drawing.Size(208, 20)
        Me.cbjamkerja.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(30, 67)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl1.TabIndex = 9
        Me.LabelControl1.Text = "Jam Kerja :"
        '
        'thit
        '
        Me.thit.Location = New System.Drawing.Point(90, 90)
        Me.thit.Name = "thit"
        Me.thit.Properties.Appearance.Options.UseTextOptions = True
        Me.thit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.thit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.thit.Properties.DisplayFormat.FormatString = "n0"
        Me.thit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.thit.Properties.EditFormat.FormatString = "n0"
        Me.thit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.thit.Properties.Mask.EditMask = "n0"
        Me.thit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.thit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.thit.Size = New System.Drawing.Size(44, 20)
        Me.thit.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(20, 93)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "Hit Jam Krja :"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(140, 93)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(154, 13)
        Me.LabelControl5.TabIndex = 12
        Me.LabelControl5.Text = "Jam, Selebihnya dihitung lembur"
        '
        'fkaryawan5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(317, 179)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.thit)
        Me.Controls.Add(Me.cbjamkerja)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btok)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fkaryawan5"
        Me.Text = "Shift"
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbjamkerja.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.thit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cbjamkerja As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents thit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
End Class
