<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class futil_mesin
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
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.btoff = New DevExpress.XtraEditors.SimpleButton()
        Me.btrest = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.btset = New DevExpress.XtraEditors.SimpleButton()
        Me.btget = New DevExpress.XtraEditors.SimpleButton()
        Me.ttime = New DevExpress.XtraEditors.TimeEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tid3 = New DevExpress.XtraEditors.TextEdit()
        Me.tid2 = New DevExpress.XtraEditors.TextEdit()
        Me.tid1 = New DevExpress.XtraEditors.TextEdit()
        Me.tip3 = New DevExpress.XtraEditors.TextEdit()
        Me.btconn3 = New DevExpress.XtraEditors.SimpleButton()
        Me.tport3 = New DevExpress.XtraEditors.TextEdit()
        Me.tip2 = New DevExpress.XtraEditors.TextEdit()
        Me.btconn2 = New DevExpress.XtraEditors.SimpleButton()
        Me.tport2 = New DevExpress.XtraEditors.TextEdit()
        Me.tip = New DevExpress.XtraEditors.TextEdit()
        Me.btconn = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tport = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cidmes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.ttime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.tid3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tid2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tid1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tip3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tport3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tip2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tport2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage2
        Me.XtraTabControl1.Size = New System.Drawing.Size(360, 225)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage2, Me.XtraTabPage1})
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.GroupControl3)
        Me.XtraTabPage2.Controls.Add(Me.GroupControl2)
        Me.XtraTabPage2.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(354, 197)
        Me.XtraTabPage2.Text = "Set Device"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.btoff)
        Me.GroupControl3.Controls.Add(Me.btrest)
        Me.GroupControl3.Location = New System.Drawing.Point(52, 334)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(346, 88)
        Me.GroupControl3.TabIndex = 8
        Me.GroupControl3.Text = "Control..."
        Me.GroupControl3.Visible = False
        '
        'btoff
        '
        Me.btoff.Location = New System.Drawing.Point(177, 41)
        Me.btoff.Name = "btoff"
        Me.btoff.Size = New System.Drawing.Size(101, 23)
        Me.btoff.TabIndex = 159
        Me.btoff.Text = "&Power Off Machine"
        '
        'btrest
        '
        Me.btrest.Location = New System.Drawing.Point(59, 41)
        Me.btrest.Name = "btrest"
        Me.btrest.Size = New System.Drawing.Size(101, 23)
        Me.btrest.TabIndex = 158
        Me.btrest.Text = "&Restart Machine"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.btset)
        Me.GroupControl2.Controls.Add(Me.btget)
        Me.GroupControl2.Controls.Add(Me.ttime)
        Me.GroupControl2.Controls.Add(Me.LabelControl4)
        Me.GroupControl2.Controls.Add(Me.LabelControl3)
        Me.GroupControl2.Controls.Add(Me.ttgl)
        Me.GroupControl2.Location = New System.Drawing.Point(52, 228)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(346, 100)
        Me.GroupControl2.TabIndex = 7
        Me.GroupControl2.Text = "Time..."
        Me.GroupControl2.Visible = False
        '
        'btset
        '
        Me.btset.Location = New System.Drawing.Point(228, 59)
        Me.btset.Name = "btset"
        Me.btset.Size = New System.Drawing.Size(84, 23)
        Me.btset.TabIndex = 158
        Me.btset.Text = "&Set Time"
        '
        'btget
        '
        Me.btget.Location = New System.Drawing.Point(228, 27)
        Me.btget.Name = "btget"
        Me.btget.Size = New System.Drawing.Size(84, 23)
        Me.btget.TabIndex = 157
        Me.btget.Text = "&Get Time"
        '
        'ttime
        '
        Me.ttime.EditValue = New Date(2013, 8, 7, 0, 0, 0, 0)
        Me.ttime.Location = New System.Drawing.Point(69, 56)
        Me.ttime.Name = "ttime"
        Me.ttime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttime.Size = New System.Drawing.Size(127, 20)
        Me.ttime.TabIndex = 156
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(35, 59)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(26, 13)
        Me.LabelControl4.TabIndex = 155
        Me.LabelControl4.Text = "Jam :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(16, 27)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl3.TabIndex = 154
        Me.LabelControl3.Text = "Tanggal :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(69, 24)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Size = New System.Drawing.Size(127, 20)
        Me.ttgl.TabIndex = 153
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.tid3)
        Me.GroupControl1.Controls.Add(Me.tid2)
        Me.GroupControl1.Controls.Add(Me.tid1)
        Me.GroupControl1.Controls.Add(Me.tip3)
        Me.GroupControl1.Controls.Add(Me.btconn3)
        Me.GroupControl1.Controls.Add(Me.tport3)
        Me.GroupControl1.Controls.Add(Me.tip2)
        Me.GroupControl1.Controls.Add(Me.btconn2)
        Me.GroupControl1.Controls.Add(Me.tport2)
        Me.GroupControl1.Controls.Add(Me.tip)
        Me.GroupControl1.Controls.Add(Me.btconn)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.tport)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(346, 189)
        Me.GroupControl1.TabIndex = 6
        Me.GroupControl1.Text = "Connection..."
        '
        'tid3
        '
        Me.tid3.Location = New System.Drawing.Point(319, 130)
        Me.tid3.Name = "tid3"
        Me.tid3.Size = New System.Drawing.Size(19, 20)
        Me.tid3.TabIndex = 14
        Me.tid3.Visible = False
        '
        'tid2
        '
        Me.tid2.Location = New System.Drawing.Point(319, 93)
        Me.tid2.Name = "tid2"
        Me.tid2.Size = New System.Drawing.Size(19, 20)
        Me.tid2.TabIndex = 13
        Me.tid2.Visible = False
        '
        'tid1
        '
        Me.tid1.Location = New System.Drawing.Point(319, 54)
        Me.tid1.Name = "tid1"
        Me.tid1.Size = New System.Drawing.Size(19, 20)
        Me.tid1.TabIndex = 12
        Me.tid1.Visible = False
        '
        'tip3
        '
        Me.tip3.Location = New System.Drawing.Point(17, 128)
        Me.tip3.Name = "tip3"
        Me.tip3.Size = New System.Drawing.Size(127, 20)
        Me.tip3.TabIndex = 9
        '
        'btconn3
        '
        Me.btconn3.Location = New System.Drawing.Point(218, 125)
        Me.btconn3.Name = "btconn3"
        Me.btconn3.Size = New System.Drawing.Size(95, 25)
        Me.btconn3.TabIndex = 11
        Me.btconn3.Text = "[TEST] Connect"
        '
        'tport3
        '
        Me.tport3.Location = New System.Drawing.Point(150, 128)
        Me.tport3.Name = "tport3"
        Me.tport3.Properties.DisplayFormat.FormatString = "f0"
        Me.tport3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tport3.Properties.EditFormat.FormatString = "f0"
        Me.tport3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tport3.Properties.Mask.EditMask = "f0"
        Me.tport3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tport3.Size = New System.Drawing.Size(63, 20)
        Me.tport3.TabIndex = 10
        '
        'tip2
        '
        Me.tip2.Location = New System.Drawing.Point(17, 91)
        Me.tip2.Name = "tip2"
        Me.tip2.Size = New System.Drawing.Size(127, 20)
        Me.tip2.TabIndex = 6
        '
        'btconn2
        '
        Me.btconn2.Location = New System.Drawing.Point(218, 88)
        Me.btconn2.Name = "btconn2"
        Me.btconn2.Size = New System.Drawing.Size(95, 25)
        Me.btconn2.TabIndex = 8
        Me.btconn2.Text = "[TEST] Connect"
        '
        'tport2
        '
        Me.tport2.Location = New System.Drawing.Point(150, 91)
        Me.tport2.Name = "tport2"
        Me.tport2.Properties.DisplayFormat.FormatString = "f0"
        Me.tport2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tport2.Properties.EditFormat.FormatString = "f0"
        Me.tport2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tport2.Properties.Mask.EditMask = "f0"
        Me.tport2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tport2.Size = New System.Drawing.Size(63, 20)
        Me.tport2.TabIndex = 7
        '
        'tip
        '
        Me.tip.Location = New System.Drawing.Point(17, 54)
        Me.tip.Name = "tip"
        Me.tip.Size = New System.Drawing.Size(127, 20)
        Me.tip.TabIndex = 1
        '
        'btconn
        '
        Me.btconn.Location = New System.Drawing.Point(218, 51)
        Me.btconn.Name = "btconn"
        Me.btconn.Size = New System.Drawing.Size(95, 25)
        Me.btconn.TabIndex = 5
        Me.btconn.Text = "[TEST] Connect"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(17, 35)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Ip Mesin :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(150, 35)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Port :"
        '
        'tport
        '
        Me.tport.Location = New System.Drawing.Point(150, 54)
        Me.tport.Name = "tport"
        Me.tport.Properties.DisplayFormat.FormatString = "f0"
        Me.tport.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tport.Properties.EditFormat.FormatString = "f0"
        Me.tport.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tport.Properties.Mask.EditMask = "f0"
        Me.tport.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tport.Size = New System.Drawing.Size(63, 20)
        Me.tport.TabIndex = 3
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.grid1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.PageVisible = False
        Me.XtraTabPage1.Size = New System.Drawing.Size(354, 197)
        Me.XtraTabPage1.Text = "User On Machine"
        '
        'grid1
        '
        Me.grid1.Location = New System.Drawing.Point(57, 63)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3})
        Me.grid1.Size = New System.Drawing.Size(223, 171)
        Me.grid1.TabIndex = 129
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cidmes, Me.cnama})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cidmes
        '
        Me.cidmes.Caption = "IdMesin"
        Me.cidmes.FieldName = "idmesin"
        Me.cidmes.Name = "cidmes"
        Me.cidmes.OptionsColumn.AllowEdit = False
        Me.cidmes.Visible = True
        Me.cidmes.VisibleIndex = 0
        Me.cidmes.Width = 34
        '
        'cnama
        '
        Me.cnama.Caption = "Nama"
        Me.cnama.FieldName = "nama"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 1
        Me.cnama.Width = 106
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
        'futil_mesin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 225)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Name = "futil_mesin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mesin Absensi"
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.ttime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.tid3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tid2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tid1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tip3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tport3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tip2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tport2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tip As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tport As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btconn As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ttime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents btset As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btget As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btoff As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btrest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cidmes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents tip2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btconn2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tport2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tip3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btconn3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tport3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tid3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tid2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tid1 As DevExpress.XtraEditors.TextEdit
End Class
