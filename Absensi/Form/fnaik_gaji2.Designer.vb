<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fnaik_gaji2
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
        Me.cbdepart = New DevExpress.XtraEditors.LookUpEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.cbkary = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cbjenis = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.cbgol = New DevExpress.XtraEditors.LookUpEdit()
        Me.cb_perubahan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tgapok = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.cek1 = New System.Windows.Forms.CheckBox()
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbkary.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbjenis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_perubahan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tgapok.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbdepart
        '
        Me.cbdepart.Location = New System.Drawing.Point(128, 49)
        Me.cbdepart.Name = "cbdepart"
        Me.cbdepart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbdepart.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbdepart.Properties.DisplayMember = "nama"
        Me.cbdepart.Properties.NullText = ""
        Me.cbdepart.Properties.ValueMember = "nama"
        Me.cbdepart.Size = New System.Drawing.Size(257, 20)
        Me.cbdepart.TabIndex = 1
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(318, 129)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(67, 27)
        Me.btclose.TabIndex = 6
        Me.btclose.Text = "&Close"
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(245, 129)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(67, 27)
        Me.btok.TabIndex = 5
        Me.btok.Text = "&OK"
        '
        'cbkary
        '
        Me.cbkary.EditValue = ""
        Me.cbkary.Location = New System.Drawing.Point(128, 49)
        Me.cbkary.Name = "cbkary"
        Me.cbkary.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbkary.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nip", 10, "NIP"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("golongan", "Golongan")})
        Me.cbkary.Properties.DisplayMember = "nama"
        Me.cbkary.Properties.NullText = ""
        Me.cbkary.Properties.ValueMember = "nip"
        Me.cbkary.Size = New System.Drawing.Size(257, 20)
        Me.cbkary.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(88, 52)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 51
        Me.LabelControl1.Text = "Nama :"
        '
        'cbjenis
        '
        Me.cbjenis.Location = New System.Drawing.Point(128, 23)
        Me.cbjenis.Name = "cbjenis"
        Me.cbjenis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbjenis.Properties.Items.AddRange(New Object() {"Golongan", "Departemen", "Karyawan"})
        Me.cbjenis.Size = New System.Drawing.Size(257, 20)
        Me.cbjenis.TabIndex = 0
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(48, 26)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(74, 13)
        Me.LabelControl10.TabIndex = 50
        Me.LabelControl10.Text = "Perubahan By :"
        '
        'cbgol
        '
        Me.cbgol.Location = New System.Drawing.Point(128, 49)
        Me.cbgol.Name = "cbgol"
        Me.cbgol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgol.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", 7, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbgol.Properties.DisplayMember = "nama"
        Me.cbgol.Properties.NullText = ""
        Me.cbgol.Properties.ValueMember = "kode"
        Me.cbgol.Size = New System.Drawing.Size(257, 20)
        Me.cbgol.TabIndex = 1
        '
        'cb_perubahan
        '
        Me.cb_perubahan.Location = New System.Drawing.Point(128, 75)
        Me.cb_perubahan.Name = "cb_perubahan"
        Me.cb_perubahan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_perubahan.Properties.Items.AddRange(New Object() {"=", "+"})
        Me.cb_perubahan.Size = New System.Drawing.Size(43, 20)
        Me.cb_perubahan.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(20, 78)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(102, 13)
        Me.LabelControl2.TabIndex = 54
        Me.LabelControl2.Text = "Jnis Prubahan + Val :"
        '
        'tgapok
        '
        Me.tgapok.Location = New System.Drawing.Point(176, 75)
        Me.tgapok.Name = "tgapok"
        Me.tgapok.Properties.Appearance.Options.UseTextOptions = True
        Me.tgapok.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.tgapok.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tgapok.Properties.DisplayFormat.FormatString = "n0"
        Me.tgapok.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tgapok.Properties.EditFormat.FormatString = "n0"
        Me.tgapok.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tgapok.Properties.Mask.EditMask = "n0"
        Me.tgapok.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tgapok.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tgapok.Size = New System.Drawing.Size(209, 20)
        Me.tgapok.TabIndex = 3
        '
        'TextEdit1
        '
        Me.TextEdit1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextEdit1.Location = New System.Drawing.Point(0, 160)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.TextEdit1.Properties.ReadOnly = True
        Me.TextEdit1.Size = New System.Drawing.Size(403, 18)
        Me.TextEdit1.TabIndex = 55
        '
        'cek1
        '
        Me.cek1.AutoSize = True
        Me.cek1.Location = New System.Drawing.Point(128, 101)
        Me.cek1.Name = "cek1"
        Me.cek1.Size = New System.Drawing.Size(121, 17)
        Me.cek1.TabIndex = 4
        Me.cek1.Text = "&Hitung ulang lembur"
        Me.cek1.UseVisualStyleBackColor = True
        '
        'fnaik_gaji2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 178)
        Me.Controls.Add(Me.cek1)
        Me.Controls.Add(Me.TextEdit1)
        Me.Controls.Add(Me.tgapok)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.cb_perubahan)
        Me.Controls.Add(Me.cbdepart)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btok)
        Me.Controls.Add(Me.cbkary)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.cbjenis)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.cbgol)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fnaik_gaji2"
        Me.Text = "Naik Gaji 2"
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbkary.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbjenis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_perubahan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tgapok.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbdepart As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbkary As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbjenis As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbgol As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cb_perubahan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tgapok As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cek1 As System.Windows.Forms.CheckBox
End Class
