<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkalk_absen2
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cbgol = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbdepart = New DevExpress.XtraEditors.LookUpEdit()
        Me.cbpeg = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.btcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ckalk = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ckalk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(24, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Periode :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(73, 21)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(96, 20)
        Me.ttgl.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(175, 24)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(16, 13)
        Me.LabelControl2.TabIndex = 154
        Me.LabelControl2.Text = "s.d"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(197, 21)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(96, 20)
        Me.ttgl2.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(391, 128)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(49, 13)
        Me.LabelControl3.TabIndex = 156
        Me.LabelControl3.Text = "Jenis Gol :"
        Me.LabelControl3.Visible = False
        '
        'cbgol
        '
        Me.cbgol.Location = New System.Drawing.Point(396, 68)
        Me.cbgol.Name = "cbgol"
        Me.cbgol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgol.Properties.Items.AddRange(New Object() {"Otomatis", "Manual"})
        Me.cbgol.Size = New System.Drawing.Size(145, 20)
        Me.cbgol.TabIndex = 2
        Me.cbgol.Visible = False
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(374, 109)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl4.TabIndex = 158
        Me.LabelControl4.Text = "Departemen :"
        Me.LabelControl4.Visible = False
        '
        'cbdepart
        '
        Me.cbdepart.Location = New System.Drawing.Point(412, 152)
        Me.cbdepart.Name = "cbdepart"
        Me.cbdepart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbdepart.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbdepart.Properties.DisplayMember = "nama"
        Me.cbdepart.Properties.ValueMember = "nama"
        Me.cbdepart.Size = New System.Drawing.Size(90, 20)
        Me.cbdepart.TabIndex = 2
        Me.cbdepart.Visible = False
        '
        'cbpeg
        '
        Me.cbpeg.Location = New System.Drawing.Point(73, 47)
        Me.cbpeg.Name = "cbpeg"
        Me.cbpeg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbpeg.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nip", "NIP"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbpeg.Properties.DisplayMember = "nama"
        Me.cbpeg.Properties.ValueMember = "nip"
        Me.cbpeg.Size = New System.Drawing.Size(220, 20)
        Me.cbpeg.TabIndex = 2
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(20, 50)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl5.TabIndex = 161
        Me.LabelControl5.Text = "Pegawai :"
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(137, 131)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(75, 23)
        Me.btok.TabIndex = 4
        Me.btok.Text = "&Load"
        '
        'btcancel
        '
        Me.btcancel.Location = New System.Drawing.Point(218, 131)
        Me.btcancel.Name = "btcancel"
        Me.btcancel.Size = New System.Drawing.Size(75, 23)
        Me.btcancel.TabIndex = 5
        Me.btcancel.Text = "&Cancel"
        '
        'ckalk
        '
        Me.ckalk.Location = New System.Drawing.Point(71, 82)
        Me.ckalk.Name = "ckalk"
        Me.ckalk.Properties.Caption = "&Kalkulasi Ulang"
        Me.ckalk.Size = New System.Drawing.Size(172, 19)
        Me.ckalk.TabIndex = 3
        '
        'fkalk_absen2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(317, 166)
        Me.ControlBox = False
        Me.Controls.Add(Me.ckalk)
        Me.Controls.Add(Me.btcancel)
        Me.Controls.Add(Me.btok)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.cbpeg)
        Me.Controls.Add(Me.cbdepart)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.cbgol)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "fkalk_absen2"
        Me.Text = "Setting Search Load..."
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ckalk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbgol As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbdepart As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cbpeg As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ckalk As DevExpress.XtraEditors.CheckEdit
End Class
