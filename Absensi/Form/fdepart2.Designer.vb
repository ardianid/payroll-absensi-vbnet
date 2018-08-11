<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fdepart2
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
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btkeluar = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Nama Depart :"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(88, 9)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(229, 20)
        Me.tnama.TabIndex = 1
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(161, 58)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 2
        Me.btsimpan.Text = "&Simpan"
        '
        'btkeluar
        '
        Me.btkeluar.Location = New System.Drawing.Point(242, 58)
        Me.btkeluar.Name = "btkeluar"
        Me.btkeluar.Size = New System.Drawing.Size(75, 23)
        Me.btkeluar.TabIndex = 3
        Me.btkeluar.Text = "&Keluar"
        '
        'fdepart2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 90)
        Me.ControlBox = False
        Me.Controls.Add(Me.btkeluar)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "fdepart2"
        Me.Text = "Departemen"
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btkeluar As DevExpress.XtraEditors.SimpleButton
End Class
