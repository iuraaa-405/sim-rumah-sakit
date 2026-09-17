<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class xtraWarehouse
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel43 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel42 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lINFORMATION_OTHER = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel36 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel35 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel34 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lBILL_COUNTRY = New DevExpress.XtraReports.UI.XRLabel()
        Me.lBILL_ZIP = New DevExpress.XtraReports.UI.XRLabel()
        Me.lBILL_STATE = New DevExpress.XtraReports.UI.XRLabel()
        Me.lBILL_CITY = New DevExpress.XtraReports.UI.XRLabel()
        Me.lBILL_STREET = New DevExpress.XtraReports.UI.XRLabel()
        Me.lINFORMATION_ADDRESS = New DevExpress.XtraReports.UI.XRLabel()
        Me.lWEBSITE = New DevExpress.XtraReports.UI.XRLabel()
        Me.lOTHER = New DevExpress.XtraReports.UI.XRLabel()
        Me.lEMAIL = New DevExpress.XtraReports.UI.XRLabel()
        Me.lMOBILE = New DevExpress.XtraReports.UI.XRLabel()
        Me.lFAX = New DevExpress.XtraReports.UI.XRLabel()
        Me.lINFORMATION_CONTACT = New DevExpress.XtraReports.UI.XRLabel()
        Me.lPHONE = New DevExpress.XtraReports.UI.XRLabel()
        Me.lNAME_DISPLAY = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.lTITLE = New DevExpress.XtraReports.UI.XRLabel()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel43, Me.XrLabel42, Me.lINFORMATION_OTHER, Me.XrLabel36, Me.XrLabel35, Me.XrLabel34, Me.XrLabel33, Me.XrLabel32, Me.XrLabel31, Me.XrLabel30, Me.XrLabel29, Me.XrLabel28, Me.XrLabel27, Me.XrLabel26, Me.XrLabel25, Me.XrLabel24, Me.XrLabel23, Me.XrLabel22, Me.XrLabel21, Me.XrLabel20, Me.XrLabel19, Me.XrLabel18, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel14, Me.lBILL_COUNTRY, Me.lBILL_ZIP, Me.lBILL_STATE, Me.lBILL_CITY, Me.lBILL_STREET, Me.lINFORMATION_ADDRESS, Me.lWEBSITE, Me.lOTHER, Me.lEMAIL, Me.lMOBILE, Me.lFAX, Me.lINFORMATION_CONTACT, Me.lPHONE, Me.lNAME_DISPLAY})
        Me.Detail.HeightF = 221.875!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel43
        '
        Me.XrLabel43.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "NAME_DISPLAY")})
        Me.XrLabel43.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel43.LocationFloat = New DevExpress.Utils.PointFloat(158.9583!, 0.0!)
        Me.XrLabel43.Name = "XrLabel43"
        Me.XrLabel43.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel43.SizeF = New System.Drawing.SizeF(458.0416!, 18.0!)
        Me.XrLabel43.StylePriority.UseFont = False
        Me.XrLabel43.Text = "XrLabel43"
        '
        'XrLabel42
        '
        Me.XrLabel42.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "MEMO")})
        Me.XrLabel42.LocationFloat = New DevExpress.Utils.PointFloat(10.00007!, 198.0417!)
        Me.XrLabel42.Multiline = True
        Me.XrLabel42.Name = "XrLabel42"
        Me.XrLabel42.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel42.SizeF = New System.Drawing.SizeF(606.9999!, 18.0!)
        Me.XrLabel42.Text = "XrLabel42"
        '
        'lINFORMATION_OTHER
        '
        Me.lINFORMATION_OTHER.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lINFORMATION_OTHER.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 173.0417!)
        Me.lINFORMATION_OTHER.Name = "lINFORMATION_OTHER"
        Me.lINFORMATION_OTHER.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lINFORMATION_OTHER.SizeF = New System.Drawing.SizeF(301.0417!, 25.0!)
        Me.lINFORMATION_OTHER.StylePriority.UseFont = False
        Me.lINFORMATION_OTHER.Text = "OTHER INFORMATION"
        '
        'XrLabel36
        '
        Me.XrLabel36.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BILL_COUNTRY")})
        Me.XrLabel36.LocationFloat = New DevExpress.Utils.PointFloat(426.3749!, 143.1251!)
        Me.XrLabel36.Multiline = True
        Me.XrLabel36.Name = "XrLabel36"
        Me.XrLabel36.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel36.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel36.Text = "XrLabel36"
        '
        'XrLabel35
        '
        Me.XrLabel35.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BILL_ZIP")})
        Me.XrLabel35.LocationFloat = New DevExpress.Utils.PointFloat(426.375!, 125.1251!)
        Me.XrLabel35.Multiline = True
        Me.XrLabel35.Name = "XrLabel35"
        Me.XrLabel35.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel35.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel35.Text = "XrLabel35"
        '
        'XrLabel34
        '
        Me.XrLabel34.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BILL_STATE")})
        Me.XrLabel34.LocationFloat = New DevExpress.Utils.PointFloat(426.3749!, 107.1251!)
        Me.XrLabel34.Multiline = True
        Me.XrLabel34.Name = "XrLabel34"
        Me.XrLabel34.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel34.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel34.Text = "XrLabel34"
        '
        'XrLabel33
        '
        Me.XrLabel33.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BILL_CITY")})
        Me.XrLabel33.LocationFloat = New DevExpress.Utils.PointFloat(426.3749!, 89.12519!)
        Me.XrLabel33.Multiline = True
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel33.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel33.Text = "XrLabel33"
        '
        'XrLabel32
        '
        Me.XrLabel32.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BILL_STREET")})
        Me.XrLabel32.LocationFloat = New DevExpress.Utils.PointFloat(426.3749!, 53.12503!)
        Me.XrLabel32.Multiline = True
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel32.SizeF = New System.Drawing.SizeF(190.625!, 36.0!)
        Me.XrLabel32.Text = "XrLabel32"
        '
        'XrLabel31
        '
        Me.XrLabel31.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "WEBSITE")})
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(120.4166!, 143.1251!)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel31.Text = "XrLabel31"
        '
        'XrLabel30
        '
        Me.XrLabel30.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OTHER")})
        Me.XrLabel30.LocationFloat = New DevExpress.Utils.PointFloat(120.4167!, 125.1251!)
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel30.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel30.Text = "XrLabel30"
        '
        'XrLabel29
        '
        Me.XrLabel29.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "EMAIL")})
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(120.4166!, 107.1252!)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel29.Text = "XrLabel29"
        '
        'XrLabel28
        '
        Me.XrLabel28.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "MOBILE")})
        Me.XrLabel28.LocationFloat = New DevExpress.Utils.PointFloat(120.4166!, 89.12519!)
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel28.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel28.Text = "XrLabel28"
        '
        'XrLabel27
        '
        Me.XrLabel27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "FAX")})
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(120.4167!, 71.12503!)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel27.Text = "[FAX]"
        '
        'XrLabel26
        '
        Me.XrLabel26.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PHONE")})
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(120.4167!, 53.125!)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(190.625!, 18.0!)
        Me.XrLabel26.Text = "XrLabel26"
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(151.6667!, 0.0!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(7.291656!, 18.0!)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = ":"
        '
        'XrLabel24
        '
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(415.9583!, 143.1251!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel24.Text = ":"
        '
        'XrLabel23
        '
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(415.9583!, 125.1251!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel23.Text = ":"
        '
        'XrLabel22
        '
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(415.9583!, 107.1252!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel22.Text = ":"
        '
        'XrLabel21
        '
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(415.9583!, 89.12506!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel21.Text = ":"
        '
        'XrLabel20
        '
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(415.9583!, 53.12503!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel20.Text = ":"
        '
        'XrLabel19
        '
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 143.1251!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel19.Text = ":"
        '
        'XrLabel18
        '
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 125.1251!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel18.Text = ":"
        '
        'XrLabel17
        '
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 107.1252!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel17.Text = ":"
        '
        'XrLabel16
        '
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 89.12512!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel16.Text = ":"
        '
        'XrLabel15
        '
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 71.12503!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel15.Text = ":"
        '
        'XrLabel14
        '
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 53.12503!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(10.41666!, 18.0!)
        Me.XrLabel14.Text = ":"
        '
        'lBILL_COUNTRY
        '
        Me.lBILL_COUNTRY.LocationFloat = New DevExpress.Utils.PointFloat(315.9583!, 143.1252!)
        Me.lBILL_COUNTRY.Name = "lBILL_COUNTRY"
        Me.lBILL_COUNTRY.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lBILL_COUNTRY.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lBILL_COUNTRY.Text = "Country"
        '
        'lBILL_ZIP
        '
        Me.lBILL_ZIP.LocationFloat = New DevExpress.Utils.PointFloat(315.9583!, 125.1251!)
        Me.lBILL_ZIP.Name = "lBILL_ZIP"
        Me.lBILL_ZIP.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lBILL_ZIP.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lBILL_ZIP.Text = "Zip Code"
        '
        'lBILL_STATE
        '
        Me.lBILL_STATE.LocationFloat = New DevExpress.Utils.PointFloat(315.9583!, 107.1251!)
        Me.lBILL_STATE.Name = "lBILL_STATE"
        Me.lBILL_STATE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lBILL_STATE.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lBILL_STATE.Text = "State"
        '
        'lBILL_CITY
        '
        Me.lBILL_CITY.LocationFloat = New DevExpress.Utils.PointFloat(315.9583!, 89.12506!)
        Me.lBILL_CITY.Name = "lBILL_CITY"
        Me.lBILL_CITY.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lBILL_CITY.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lBILL_CITY.Text = "City"
        '
        'lBILL_STREET
        '
        Me.lBILL_STREET.LocationFloat = New DevExpress.Utils.PointFloat(315.9583!, 53.125!)
        Me.lBILL_STREET.Name = "lBILL_STREET"
        Me.lBILL_STREET.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lBILL_STREET.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lBILL_STREET.Text = "Street"
        '
        'lINFORMATION_ADDRESS
        '
        Me.lINFORMATION_ADDRESS.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lINFORMATION_ADDRESS.LocationFloat = New DevExpress.Utils.PointFloat(315.9583!, 28.125!)
        Me.lINFORMATION_ADDRESS.Name = "lINFORMATION_ADDRESS"
        Me.lINFORMATION_ADDRESS.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lINFORMATION_ADDRESS.SizeF = New System.Drawing.SizeF(301.0418!, 25.0!)
        Me.lINFORMATION_ADDRESS.StylePriority.UseFont = False
        Me.lINFORMATION_ADDRESS.Text = "ADDRESS INFORMATION"
        '
        'lWEBSITE
        '
        Me.lWEBSITE.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 143.1252!)
        Me.lWEBSITE.Name = "lWEBSITE"
        Me.lWEBSITE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lWEBSITE.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lWEBSITE.Text = "Website"
        '
        'lOTHER
        '
        Me.lOTHER.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 125.1251!)
        Me.lOTHER.Name = "lOTHER"
        Me.lOTHER.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lOTHER.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lOTHER.Text = "Other"
        '
        'lEMAIL
        '
        Me.lEMAIL.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 107.1251!)
        Me.lEMAIL.Name = "lEMAIL"
        Me.lEMAIL.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lEMAIL.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lEMAIL.Text = "Email"
        '
        'lMOBILE
        '
        Me.lMOBILE.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 89.12506!)
        Me.lMOBILE.Name = "lMOBILE"
        Me.lMOBILE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lMOBILE.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lMOBILE.Text = "Mobile"
        '
        'lFAX
        '
        Me.lFAX.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 71.12503!)
        Me.lFAX.Name = "lFAX"
        Me.lFAX.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lFAX.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lFAX.Text = "Fax"
        '
        'lINFORMATION_CONTACT
        '
        Me.lINFORMATION_CONTACT.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lINFORMATION_CONTACT.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 28.125!)
        Me.lINFORMATION_CONTACT.Name = "lINFORMATION_CONTACT"
        Me.lINFORMATION_CONTACT.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lINFORMATION_CONTACT.SizeF = New System.Drawing.SizeF(301.0417!, 25.0!)
        Me.lINFORMATION_CONTACT.StylePriority.UseFont = False
        Me.lINFORMATION_CONTACT.Text = "CONTACT INFORMATION"
        '
        'lPHONE
        '
        Me.lPHONE.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 53.125!)
        Me.lPHONE.Name = "lPHONE"
        Me.lPHONE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lPHONE.SizeF = New System.Drawing.SizeF(100.0!, 18.0!)
        Me.lPHONE.Text = "Phone"
        '
        'lNAME_DISPLAY
        '
        Me.lNAME_DISPLAY.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lNAME_DISPLAY.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.lNAME_DISPLAY.Name = "lNAME_DISPLAY"
        Me.lNAME_DISPLAY.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lNAME_DISPLAY.SizeF = New System.Drawing.SizeF(141.6667!, 18.0!)
        Me.lNAME_DISPLAY.StylePriority.UseFont = False
        Me.lNAME_DISPLAY.Text = "Display Name"
        '
        'TopMargin
        '
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.lTITLE})
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lTITLE
        '
        Me.lTITLE.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lTITLE.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 75.0!)
        Me.lTITLE.Name = "lTITLE"
        Me.lTITLE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lTITLE.SizeF = New System.Drawing.SizeF(607.0001!, 18.0!)
        Me.lTITLE.StylePriority.UseBorders = False
        Me.lTITLE.StylePriority.UseFont = False
        Me.lTITLE.StylePriority.UseTextAlignment = False
        Me.lTITLE.Text = "WAREHOUSE"
        Me.lTITLE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'BottomMargin
        '
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.M_WAREHOUSE)
        '
        'xtraVendor
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.DataSource = Me.bindingSource
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "12.1"
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents lTITLE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lNAME_DISPLAY As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lPHONE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lINFORMATION_CONTACT As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lOTHER As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lEMAIL As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lMOBILE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lFAX As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lWEBSITE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lINFORMATION_ADDRESS As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lBILL_STREET As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lBILL_COUNTRY As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lBILL_ZIP As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lBILL_STATE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lBILL_CITY As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel36 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel35 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel34 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lINFORMATION_OTHER As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel42 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel43 As DevExpress.XtraReports.UI.XRLabel
End Class
