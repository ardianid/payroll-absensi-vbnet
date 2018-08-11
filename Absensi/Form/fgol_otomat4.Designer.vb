<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgol_otomat4
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
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.bttambah = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(26, 27)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl4.TabIndex = 14
        Me.LabelControl4.Text = "Kode :"
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(63, 24)
        Me.tkode.Name = "tkode"
        Me.tkode.Size = New System.Drawing.Size(271, 20)
        Me.tkode.TabIndex = 0
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(63, 46)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(271, 20)
        Me.tnama.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(23, 49)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 16
        Me.LabelControl1.Text = "Nama :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(275, 82)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(59, 23)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Close"
        '
        'bttambah
        '
        Me.bttambah.Location = New System.Drawing.Point(210, 82)
        Me.bttambah.Name = "bttambah"
        Me.bttambah.Size = New System.Drawing.Size(59, 23)
        Me.bttambah.TabIndex = 2
        Me.bttambah.Text = "&Tambah"
        '
        'fgol_otomat4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 117)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.bttambah)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fgol_otomat4"
        Me.Text = "Golongan Kerja (Jenis Kerja)"
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bttambah As DevExpress.XtraEditors.SimpleButton
End Class
