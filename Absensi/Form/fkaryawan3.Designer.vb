<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkaryawan3
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
        Me.cbhari = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        CType(Me.cbhari.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(36, 48)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(26, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Hari :"
        '
        'cbhari
        '
        Me.cbhari.Location = New System.Drawing.Point(68, 45)
        Me.cbhari.Name = "cbhari"
        Me.cbhari.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbhari.Properties.Items.AddRange(New Object() {"SENIN", "SELASA", "RABU", "KAMIS", "JUMAT", "SABTU", "MINGGU"})
        Me.cbhari.Size = New System.Drawing.Size(227, 20)
        Me.cbhari.TabIndex = 2
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(167, 112)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(61, 23)
        Me.btok.TabIndex = 3
        Me.btok.Text = "Ok"
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(234, 112)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(61, 23)
        Me.btselesai.TabIndex = 4
        Me.btselesai.Text = "Selesai"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(17, 22)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Tanggal :"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(68, 19)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Size = New System.Drawing.Size(100, 20)
        Me.ttgl1.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(174, 22)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl3.TabIndex = 6
        Me.LabelControl3.Text = "s.d"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(195, 19)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(100, 20)
        Me.ttgl2.TabIndex = 1
        '
        'fkaryawan3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 151)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btok)
        Me.Controls.Add(Me.cbhari)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fkaryawan3"
        Me.Text = "Hari Libur"
        CType(Me.cbhari.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbhari As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
End Class
