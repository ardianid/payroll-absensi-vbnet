<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ftidakhadir
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
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.btcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(158, 71)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(65, 29)
        Me.btload.TabIndex = 167
        Me.btload.Text = "&Preview"
        '
        'btcancel
        '
        Me.btcancel.Location = New System.Drawing.Point(229, 71)
        Me.btcancel.Name = "btcancel"
        Me.btcancel.Size = New System.Drawing.Size(65, 29)
        Me.btcancel.TabIndex = 168
        Me.btcancel.Text = "&Keluar"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(198, 22)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(96, 20)
        Me.ttgl2.TabIndex = 166
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl1.Location = New System.Drawing.Point(174, 25)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(18, 13)
        Me.LabelControl1.TabIndex = 170
        Me.LabelControl1.Text = "S.D"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(72, 22)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.Mask.EditMask = ""
        Me.ttgl1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Size = New System.Drawing.Size(96, 20)
        Me.ttgl1.TabIndex = 165
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl3.Location = New System.Drawing.Point(23, 25)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl3.TabIndex = 169
        Me.LabelControl3.Text = "Periode :"
        '
        'ftidakhadir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(323, 109)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.btcancel)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.LabelControl3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ftidakhadir"
        Me.Text = "Karyawan Tidak Masuk"
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
End Class
