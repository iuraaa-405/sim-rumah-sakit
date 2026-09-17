Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmBrowseItem_History_In
    Private oType As Integer = 0
    Private oVENDOR As String = ""

    Public Sub fn_LoadMe(ByVal FindType As Integer, ByVal KdVENDOR As String)
        oType = FindType
        oVENDOR = KdVENDOR
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            sFind1 = String.Empty
            sFind2 = String.Empty
            Me.Close()
        End If
    End Sub

    Private Sub fn_LoadGrid()
        Dim oData As New Purchasing.clsPurchaseInvoice
        Try
            Try
                Dim ds = From x In oData.GetData _
                         Join y In oData.GetDataDetail _
                         On x.KDPI Equals y.KDPI _
                         Where x.KDVENDOR = oVENDOR _
                         Select Barcode = y.M_ITEM.NMITEM1, Nama = y.M_ITEM.NMITEM2, Jumlah = y.QTY, Harga = y.PRICE, NoRef = x.KDPI _
                         Order By Nama Ascending, NoRef Descending

                grd.DataSource = ds.ToList
                grd.RefreshDataSource()
            Catch ex As Exception
            End Try
        Catch oErr As Exception
            MsgBox("Load Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class