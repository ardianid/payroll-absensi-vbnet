<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fshift3
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tjamkerja = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tmasuk = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tpulang = New DevExpress.XtraEditors.TextEdit()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbcekk = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tjamkerja.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmasuk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpulang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbcekk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(11, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Jam Kerja :"
        '
        'tjamkerja
        '
        Me.tjamkerja.Location = New System.Drawing.Point(71, 12)
        Me.tjamkerja.Name = "tjamkerja"
        Me.tjamkerja.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tjamkerja.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", "Kode", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", 15, "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jammasuk", 10, "Jm Masuk"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jampulang", 10, "Jm Pulang")})
        Me.tjamkerja.Properties.DisplayMember = "nama"
        '   Me.tjamkerja.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tjamkerja.Properties.NullText = ""
        Me.tjamkerja.Properties.ValueMember = "kode"
        Me.tjamkerja.Size = New System.Drawing.Size(225, 20)
        Me.tjamkerja.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(11, 38)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Jm Masuk :"
        '
        'tmasuk
        '
        Me.tmasuk.Enabled = False
        Me.tmasuk.Location = New System.Drawing.Point(70, 35)
        Me.tmasuk.Name = "tmasuk"
        '   Me.tmasuk.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tmasuk.Size = New System.Drawing.Size(78, 20)
        Me.tmasuk.TabIndex = 5
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(157, 38)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl3.TabIndex = 6
        Me.LabelControl3.Text = "Jm Pulang :"
        '
        'tpulang
        '
        Me.tpulang.Enabled = False
        Me.tpulang.Location = New System.Drawing.Point(218, 35)
        Me.tpulang.Name = "tpulang"
        '    Me.tpulang.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tpulang.Size = New System.Drawing.Size(78, 20)
        Me.tpulang.TabIndex = 7
        '
        'grid1
        '
        Me.grid1.Cursor = System.Windows.Forms.Cursors.Default
        Me.grid1.Location = New System.Drawing.Point(11, 57)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.grid1.Size = New System.Drawing.Size(351, 288)
        Me.grid1.TabIndex = 8
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn1})
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        StyleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        StyleFormatCondition1.Column = Me.GridColumn2
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = "MINGGU"
        Me.GridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowColumnHeaders = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsView.ShowIndicator = False
        Me.GridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.FieldName = "nama_periode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 479
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Chk?"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn1.FieldName = "pakai"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 47
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.DisplayValueChecked = "1"
        Me.RepositoryItemCheckEdit1.DisplayValueUnchecked = "0"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = 1
        Me.RepositoryItemCheckEdit1.ValueUnchecked = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(11, 351)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl4.TabIndex = 9
        Me.LabelControl4.Text = "Option :"
        '
        'cbcekk
        '
        Me.cbcekk.Location = New System.Drawing.Point(56, 348)
        Me.cbcekk.Name = "cbcekk"
        Me.cbcekk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbcekk.Properties.Items.AddRange(New Object() {"Cek All", "Un-Cek All"})
        '   Me.cbcekk.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.cbcekk.Size = New System.Drawing.Size(104, 20)
        Me.cbcekk.TabIndex = 10
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(287, 372)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(75, 25)
        Me.btselesai.TabIndex = 12
        Me.btselesai.Text = "Se&lesai"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(206, 372)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 11
        Me.btsimpan.Text = "&OK"
        '
        'fshift3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(374, 409)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.cbcekk)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tpulang)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tjamkerja)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tmasuk)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fshift3"
        Me.Text = "Shift Karyawan"
        CType(Me.tjamkerja.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmasuk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpulang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbcekk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjamkerja As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tmasuk As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tpulang As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbcekk As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
