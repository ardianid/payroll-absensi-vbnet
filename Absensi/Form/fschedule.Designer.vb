<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fschedule
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fschedule))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Me.clibur = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.bn1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.cbtahun = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsload = New System.Windows.Forms.ToolStripButton()
        Me.tssimpan = New System.Windows.Forms.ToolStripButton()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ctanggal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cminggu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chari = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cket = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cketlibur = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bn1.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'clibur
        '
        Me.clibur.AppearanceHeader.Options.UseTextOptions = True
        Me.clibur.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.clibur.Caption = "Libur?"
        Me.clibur.ColumnEdit = Me.RepositoryItemCheckEdit3
        Me.clibur.FieldName = "libur"
        Me.clibur.Name = "clibur"
        Me.clibur.Visible = True
        Me.clibur.VisibleIndex = 3
        Me.clibur.Width = 32
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.DisplayValueChecked = "1"
        Me.RepositoryItemCheckEdit3.DisplayValueUnchecked = "0"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = 1
        Me.RepositoryItemCheckEdit3.ValueUnchecked = 0
        '
        'bn1
        '
        Me.bn1.AddNewItem = Nothing
        Me.bn1.CountItem = Me.ToolStripLabel2
        Me.bn1.DeleteItem = Nothing
        Me.bn1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bn1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bn1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator2, Me.ToolStripTextBox1, Me.ToolStripLabel2, Me.ToolStripSeparator3, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator4, Me.ToolStripLabel3, Me.cbtahun, Me.ToolStripSeparator1, Me.tsload, Me.tssimpan})
        Me.bn1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.bn1.Location = New System.Drawing.Point(0, 0)
        Me.bn1.MoveFirstItem = Me.ToolStripButton2
        Me.bn1.MoveLastItem = Me.ToolStripButton5
        Me.bn1.MoveNextItem = Me.ToolStripButton4
        Me.bn1.MovePreviousItem = Me.ToolStripButton3
        Me.bn1.Name = "bn1"
        Me.bn1.PositionItem = Me.ToolStripTextBox1
        Me.bn1.Size = New System.Drawing.Size(701, 36)
        Me.bn1.TabIndex = 127
        Me.bn1.Text = "BindingNavigator1"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.ForeColor = System.Drawing.Color.Black
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(36, 33)
        Me.ToolStripLabel2.Text = "of {0}"
        Me.ToolStripLabel2.ToolTipText = "Total number of items"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton2.Tag = "True"
        Me.ToolStripButton2.Text = "Move first"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton3.Tag = "True"
        Me.ToolStripButton3.Text = "Move previous"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AccessibleName = "Position"
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(58, 21)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.ToolTipText = "Current position"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton4.Tag = "True"
        Me.ToolStripButton4.Text = "Move next"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton5.Tag = "True"
        Me.ToolStripButton5.Text = "Move last"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.ForeColor = System.Drawing.Color.Black
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(47, 33)
        Me.ToolStripLabel3.Text = "Tahun  :"
        '
        'cbtahun
        '
        Me.cbtahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbtahun.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cbtahun.Name = "cbtahun"
        Me.cbtahun.Size = New System.Drawing.Size(75, 36)
        Me.cbtahun.ToolTipText = "Kriteria pencarian"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 36)
        '
        'tsload
        '
        Me.tsload.AutoSize = False
        Me.tsload.ForeColor = System.Drawing.Color.Black
        Me.tsload.Image = CType(resources.GetObject("tsload.Image"), System.Drawing.Image)
        Me.tsload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsload.Name = "tsload"
        Me.tsload.Size = New System.Drawing.Size(49, 33)
        Me.tsload.Text = "&Load..."
        Me.tsload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsload.ToolTipText = "Load Otomatic"
        '
        'tssimpan
        '
        Me.tssimpan.AutoSize = False
        Me.tssimpan.ForeColor = System.Drawing.Color.Black
        Me.tssimpan.Image = CType(resources.GetObject("tssimpan.Image"), System.Drawing.Image)
        Me.tssimpan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tssimpan.Name = "tssimpan"
        Me.tssimpan.Size = New System.Drawing.Size(49, 33)
        Me.tssimpan.Text = "&Simpan"
        Me.tssimpan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tssimpan.ToolTipText = "Simpan"
        '
        'grid1
        '
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 36)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3, Me.RepositoryItemComboBox1})
        Me.grid1.Size = New System.Drawing.Size(701, 416)
        Me.grid1.TabIndex = 128
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ctanggal, Me.cminggu, Me.chari, Me.clibur, Me.cket, Me.cketlibur})
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Orange
        StyleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        StyleFormatCondition1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        StyleFormatCondition1.Appearance.Options.UseFont = True
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Column = Me.clibur
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = 1
        StyleFormatCondition1.Value2 = 1
        Me.GridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'ctanggal
        '
        Me.ctanggal.Caption = "Tanggal"
        Me.ctanggal.FieldName = "tanggal"
        Me.ctanggal.Name = "ctanggal"
        Me.ctanggal.OptionsColumn.AllowEdit = False
        Me.ctanggal.Visible = True
        Me.ctanggal.VisibleIndex = 0
        Me.ctanggal.Width = 71
        '
        'cminggu
        '
        Me.cminggu.AppearanceHeader.Options.UseTextOptions = True
        Me.cminggu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cminggu.Caption = "Minggu"
        Me.cminggu.FieldName = "minggu"
        Me.cminggu.Name = "cminggu"
        Me.cminggu.OptionsColumn.AllowEdit = False
        Me.cminggu.Visible = True
        Me.cminggu.VisibleIndex = 1
        Me.cminggu.Width = 28
        '
        'chari
        '
        Me.chari.Caption = "Hari"
        Me.chari.FieldName = "hari"
        Me.chari.Name = "chari"
        Me.chari.OptionsColumn.AllowEdit = False
        Me.chari.Visible = True
        Me.chari.VisibleIndex = 2
        Me.chari.Width = 52
        '
        'cket
        '
        Me.cket.Caption = "Note"
        Me.cket.FieldName = "ket"
        Me.cket.Name = "cket"
        Me.cket.Visible = True
        Me.cket.VisibleIndex = 5
        Me.cket.Width = 438
        '
        'cketlibur
        '
        Me.cketlibur.Caption = "Jenis Libur"
        Me.cketlibur.ColumnEdit = Me.RepositoryItemComboBox1
        Me.cketlibur.FieldName = "jenislibur"
        Me.cketlibur.Name = "cketlibur"
        Me.cketlibur.Visible = True
        Me.cketlibur.VisibleIndex = 4
        Me.cketlibur.Width = 62
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"-", "Libur Normal", "Libur Hari Besar"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'fschedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 452)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.bn1)
        Me.Name = "fschedule"
        Me.Text = "Kalender Kerja"
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bn1.ResumeLayout(False)
        Me.bn1.PerformLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bn1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cbtahun As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ctanggal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cminggu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chari As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents clibur As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cket As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tssimpan As System.Windows.Forms.ToolStripButton
    Friend WithEvents cketlibur As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
End Class
