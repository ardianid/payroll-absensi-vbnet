﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fjamkerja_oby_gol3
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
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.cbjenis = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cbkary = New DevExpress.XtraEditors.LookUpEdit()
        Me.cbgol = New DevExpress.XtraEditors.LookUpEdit()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.cbdepart = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.cbjenis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbkary.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(11, 22)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl10.TabIndex = 41
        Me.LabelControl10.Text = "Jenis By :"
        '
        'cbjenis
        '
        Me.cbjenis.Location = New System.Drawing.Point(63, 19)
        Me.cbjenis.Name = "cbjenis"
        Me.cbjenis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbjenis.Properties.Items.AddRange(New Object() {"Golongan", "Departemen", "Karyawan"})
        Me.cbjenis.Size = New System.Drawing.Size(226, 20)
        Me.cbjenis.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(36, 48)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl1.TabIndex = 43
        Me.LabelControl1.Text = "Val :"
        '
        'cbkary
        '
        Me.cbkary.EditValue = ""
        Me.cbkary.Location = New System.Drawing.Point(63, 45)
        Me.cbkary.Name = "cbkary"
        Me.cbkary.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbkary.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nip", 10, "NIP"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("golongan", "Golongan")})
        Me.cbkary.Properties.DisplayMember = "nama"
        Me.cbkary.Properties.NullText = ""
        Me.cbkary.Properties.ValueMember = "nip"
        Me.cbkary.Size = New System.Drawing.Size(226, 20)
        Me.cbkary.TabIndex = 2
        '
        'cbgol
        '
        Me.cbgol.Location = New System.Drawing.Point(63, 45)
        Me.cbgol.Name = "cbgol"
        Me.cbgol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgol.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", 7, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbgol.Properties.DisplayMember = "nama"
        Me.cbgol.Properties.NullText = ""
        Me.cbgol.Properties.ValueMember = "kode"
        Me.cbgol.Size = New System.Drawing.Size(226, 20)
        Me.cbgol.TabIndex = 1
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(149, 88)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(67, 23)
        Me.btok.TabIndex = 3
        Me.btok.Text = "&OK"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(222, 88)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(67, 23)
        Me.btclose.TabIndex = 4
        Me.btclose.Text = "&Close"
        '
        'cbdepart
        '
        Me.cbdepart.Location = New System.Drawing.Point(63, 45)
        Me.cbdepart.Name = "cbdepart"
        Me.cbdepart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbdepart.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbdepart.Properties.DisplayMember = "nama"
        Me.cbdepart.Properties.NullText = ""
        Me.cbdepart.Properties.ValueMember = "nama"
        Me.cbdepart.Size = New System.Drawing.Size(226, 20)
        Me.cbdepart.TabIndex = 44
        '
        'fjamkerja_oby_gol3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(305, 121)
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
        Me.Name = "fjamkerja_oby_gol3"
        Me.Text = "Pegawai (Jam Kerja Diluar Standar)"
        CType(Me.cbjenis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbkary.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbjenis As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbkary As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cbgol As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbdepart As DevExpress.XtraEditors.LookUpEdit
End Class
