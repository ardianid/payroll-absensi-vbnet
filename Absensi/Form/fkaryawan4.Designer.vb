<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkaryawan4
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
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.cbjamkerja = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.cbjamkerja.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(19, 17)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Jam Kerja :"
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(226, 59)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(61, 23)
        Me.btselesai.TabIndex = 2
        Me.btselesai.Text = "Selesai"
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(159, 59)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(61, 23)
        Me.btok.TabIndex = 1
        Me.btok.Text = "Ok"
        '
        'cbjamkerja
        '
        Me.cbjamkerja.Location = New System.Drawing.Point(79, 14)
        Me.cbjamkerja.Name = "cbjamkerja"
        Me.cbjamkerja.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbjamkerja.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbjamkerja.Properties.DisplayMember = "nama"
        Me.cbjamkerja.Properties.NullText = ""
        Me.cbjamkerja.Properties.PopupSizeable = False
        Me.cbjamkerja.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbjamkerja.Properties.ValueMember = "kode"
        Me.cbjamkerja.Size = New System.Drawing.Size(208, 20)
        Me.cbjamkerja.TabIndex = 0
        '
        'fkaryawan4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 90)
        Me.Controls.Add(Me.cbjamkerja)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btok)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fkaryawan4"
        Me.Text = "Jam Kerja Pegawai"
        CType(Me.cbjamkerja.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbjamkerja As DevExpress.XtraEditors.LookUpEdit
End Class
