<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkontrak_periode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.btcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl1.Location = New System.Drawing.Point(17, 53)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl1.TabIndex = 179
        Me.LabelControl1.Text = "Tgl Akhir :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl3.Location = New System.Drawing.Point(18, 27)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl3.TabIndex = 178
        Me.LabelControl3.Text = "Tgl Awal :"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(71, 24)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Properties.Mask.EditMask = ""
        Me.ttgl1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl1.Size = New System.Drawing.Size(125, 20)
        Me.ttgl1.TabIndex = 0
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(71, 50)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Size = New System.Drawing.Size(125, 20)
        Me.ttgl2.TabIndex = 1
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(76, 98)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(57, 28)
        Me.btload.TabIndex = 2
        Me.btload.Text = "&Load"
        '
        'btcancel
        '
        Me.btcancel.Location = New System.Drawing.Point(139, 98)
        Me.btcancel.Name = "btcancel"
        Me.btcancel.Size = New System.Drawing.Size(57, 28)
        Me.btcancel.TabIndex = 3
        Me.btcancel.Text = "&Keluar"
        '
        'fkontrak_periode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(218, 147)
        Me.Controls.Add(Me.btcancel)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.ttgl2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fkontrak_periode"
        Me.Text = "Kontrak Akhir by Periode"
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btcancel As DevExpress.XtraEditors.SimpleButton
End Class
