Imports System.Data
Imports System.Data.SqlClient

Namespace Accounting
    Public Class clsStockCard
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "STOCKCARD"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_STOCK_CARD
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_STOCK_CARD
        End Function
        Public Function GetStructureDetailList() As List(Of A_STOCK_CARD_NEW)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of A_STOCK_CARD_NEW)
        End Function
        Public Function GetStructureDetail() As A_STOCK_CARD_NEW
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New A_STOCK_CARD_NEW
        End Function
        Public Function GetData() As List(Of A_STOCK_CARD)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_STOCK_CARDs.ToList()
        End Function
        Public Function GetData(ByVal sKDITEM As String) As IQueryable(Of A_STOCK_CARD)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_STOCK_CARDs.Where(Function(x) x.KDITEM = sKDITEM)
        End Function
        'Public Function GetData(ByVal sKDITEM As String, ByVal sKDUOM As String) As IQueryable(Of A_STOCK_CARD)
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.A_STOCK_CARDs.Where(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM)
        'End Function
        'Public Function PostingAverage(ByVal sKDITEM As List(Of String)) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            PostingAverage = False
        '            Exit Function
        '        End If

        '        For Each xLoop In sKDITEM.Distinct
        '            sREFERENCE = xLoop
        '            sSTATUS = "POSTINGAVERAGE"

        '            Dim Average As Decimal = 0
        '            Dim QtyBalance As Decimal = 0
        '            Dim PriceBalance As Decimal = 0
        '            Dim TotalBalance As Decimal = 0
        '            Dim sSeq = 1
        '            Dim sItem = xLoop

        '            Dim oConn As New SqlConnection
        '            Dim oComm As New SqlCommand
        '            Dim da As SqlDataAdapter
        '            Dim ds As New DataSet
        '            Dim SQL As String

        '            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

        '            oConn = New SqlConnection(sConn)
        '            If oConn.State = ConnectionState.Closed Then
        '                oConn.Open()
        '            End If

        '            SQL = "SELECT * FROM ("
        '            SQL &= "(SELECT "
        '            SQL &= "NOID = A.KDPI "
        '            SQL &= ",NOREF = A.KDPI "
        '            SQL &= ",[DATE] = A.DATE "
        '            SQL &= ",KDITEM = B.KDITEM "
        '            SQL &= ",KDUOM = B.KDUOM "
        '            SQL &= ",QTY = B.QTY * C.RATE "
        '            'SQL &= ",HPP = (B.GRANDTOTAL / (B.QTY * C.RATE)) - (A.DISCOUNT / (B.QTY * C.RATE)) + (A.TAX / (B.QTY * C.RATE)) "
        '            'SQL &= ",TOTAL = (B.GRANDTOTAL / (B.QTY * C.RATE)) - (A.DISCOUNT / (B.QTY * C.RATE)) + (A.TAX / (B.QTY * C.RATE)) * (B.QTY * C.RATE) "
        '            SQL &= ",HPP = (B.GRANDTOTAL / (B.QTY * C.RATE)) - (A.DISCOUNT / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PI_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPI = A.KDPI)) + (A.TAX / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PI_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPI = A.KDPI)) "
        '            SQL &= ",TOTAL = (B.GRANDTOTAL / (B.QTY * C.RATE)) - (A.DISCOUNT / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PI_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPI = A.KDPI)) + (A.TAX / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PI_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPI = A.KDPI)) * (B.QTY * C.RATE) "
        '            SQL &= ",TIPE = '001' "
        '            SQL &= ",SEQREF = B.SEQ "
        '            SQL &= "FROM P_PI_H A "
        '            SQL &= "INNER JOIN P_PI_D B "
        '            SQL &= "ON B.KDPI = A.KDPI "
        '            SQL &= "INNER JOIN M_ITEM_UOM C "
        '            SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
        '            SQL &= "WHERE B.KDITEM = '" & xLoop & "'"
        '            SQL &= ") "
        '            SQL &= "UNION "
        '            SQL &= "(SELECT "
        '            SQL &= "NOID = A.KDPR "
        '            SQL &= ",NOREF = A.KDPR "
        '            SQL &= ",[DATE] = A.DATE "
        '            SQL &= ",KDITEM = B.KDITEM "
        '            SQL &= ",KDUOM = B.KDUOM "
        '            SQL &= ",QTY = B.QTY * C.RATE "
        '            SQL &= ",HPP = (B.GRANDTOTAL / (B.QTY * C.RATE)) - (A.DISCOUNT / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PR_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPR = A.KDPR)) + (A.TAX / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PR_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPR = A.KDPR)) "
        '            SQL &= ",TOTAL = (B.GRANDTOTAL / (B.QTY * C.RATE)) - (A.DISCOUNT / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PR_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPR = A.KDPR)) + (A.TAX / (SELECT SUM(AA.QTY * BB.RATE) FROM P_PR_D AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM AND AA.KDUOM = BB.KDUOM WHERE AA.KDPR = A.KDPR)) * (B.QTY * C.RATE) "
        '            SQL &= ",TIPE = '002' "
        '            SQL &= ",SEQREF = B.SEQ "
        '            SQL &= "FROM P_PR_H A "
        '            SQL &= "INNER JOIN P_PR_D B "
        '            SQL &= "ON B.KDPR = A.KDPR "
        '            SQL &= "INNER JOIN M_ITEM_UOM C "
        '            SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
        '            SQL &= "WHERE B.KDITEM = '" & xLoop & "'"
        '            SQL &= ") "
        '            SQL &= "UNION "
        '            SQL &= "(SELECT "
        '            SQL &= "NOID = A.KDSI "
        '            SQL &= ",NOREF = A.KDSI "
        '            SQL &= ",[DATE] = A.DATE "
        '            SQL &= ",KDITEM = B.KDITEM "
        '            SQL &= ",KDUOM = B.KDUOM "
        '            SQL &= ",QTY = B.QTY * C.RATE "
        '            SQL &= ",HPP = 0 "
        '            SQL &= ",TOTAL = 0 "
        '            SQL &= ",TIPE = '003' "
        '            SQL &= ",SEQREF = B.SEQ "
        '            SQL &= "FROM S_SI_H A "
        '            SQL &= "INNER JOIN S_SI_D B "
        '            SQL &= "ON B.KDSI = A.KDSI "
        '            SQL &= "INNER JOIN M_ITEM_UOM C "
        '            SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
        '            SQL &= "WHERE B.KDITEM = '" & xLoop & "'"
        '            SQL &= ") "
        '            SQL &= "UNION "
        '            SQL &= "(SELECT "
        '            SQL &= "NOID = A.KDSR "
        '            SQL &= ",NOREF = A.KDSR "
        '            SQL &= ",[DATE] = A.DATE "
        '            SQL &= ",KDITEM = B.KDITEM "
        '            SQL &= ",KDUOM = B.KDUOM "
        '            SQL &= ",QTY = B.QTY * C.RATE "
        '            SQL &= ",HPP = 0 "
        '            SQL &= ",TOTAL = 0 "
        '            SQL &= ",TIPE = '004' "
        '            SQL &= ",SEQREF = B.SEQ "
        '            SQL &= "FROM S_SR_H A "
        '            SQL &= "INNER JOIN S_SR_D B "
        '            SQL &= "ON B.KDSR = A.KDSR "
        '            SQL &= "INNER JOIN M_ITEM_UOM C "
        '            SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
        '            SQL &= "WHERE B.KDITEM = '" & xLoop & "'"
        '            SQL &= ") "
        '            SQL &= "UNION "
        '            SQL &= "(SELECT "
        '            SQL &= "NOID = A.KDOPNAME "
        '            SQL &= ",NOREF = A.KDOPNAME "
        '            SQL &= ",[DATE] = A.DATE "
        '            SQL &= ",KDITEM = B.KDITEM "
        '            SQL &= ",KDUOM = B.KDUOM "
        '            SQL &= ",QTY = B.QTY * C.RATE "
        '            SQL &= ",HPP = 0 "
        '            SQL &= ",TOTAL = 0 "
        '            SQL &= ",TIPE = '000' "
        '            SQL &= ",SEQREF = B.SEQ "
        '            SQL &= "FROM I_OPNAME_H A "
        '            SQL &= "INNER JOIN I_OPNAME_D B "
        '            SQL &= "ON B.KDOPNAME = A.KDOPNAME "
        '            SQL &= "INNER JOIN M_ITEM_UOM C "
        '            SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
        '            SQL &= "WHERE B.KDITEM = '" & xLoop & "'"
        '            SQL &= ") "
        '            SQL &= ") AS A "
        '            SQL &= "ORDER BY CONVERT(VARCHAR(10), [DATE], 112), TIPE, SEQREF  "

        '            Try
        '                Dim dsClear = GetData(sItem)
        '                oConnection.db.A_STOCK_CARDs.DeleteAllOnSubmit(dsClear)
        '                oConnection.db.SubmitChanges()
        '            Catch ex As Exception
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try

        '            oComm.Connection = oConn
        '            oComm.CommandText = SQL
        '            oComm.CommandTimeout = 120
        '            oComm.CommandType = CommandType.Text

        '            da = New SqlDataAdapter(oComm)
        '            da.Fill(ds, "ALL")

        '            If ds.Tables("ALL").Rows.Count < 1 Then
        '                Continue For
        '            End If

        '            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
        '                Dim sSQL = "INSERT INTO A_STOCK_CARD VALUES "

        '                With ds.Tables("ALL")
        '                    Dim sTOTALIN As Decimal = 0
        '                    Dim sTOTALOUT As Decimal = 0

        '                    sSQL &= "("
        '                    sSQL &= "'" & .Rows(iLoop)("NOID") & "',"
        '                    sSQL &= "'" & CDate(.Rows(iLoop)("DATE")).ToString("yyyy-MM-dd HH:mm:ss") & "',"
        '                    sSQL &= "'" & .Rows(iLoop)("KDITEM") & "',"
        '                    sSQL &= "'" & .Rows(iLoop)("KDUOM") & "',"

        '                    If .Rows(iLoop)("TIPE") = "001" Then 'PI
        '                        sSQL &= "" & .Rows(iLoop)("QTY") & ","
        '                        sSQL &= "" & .Rows(iLoop)("HPP") & ","
        '                        sSQL &= "" & .Rows(iLoop)("QTY") * .Rows(iLoop)("HPP") & ","

        '                        sTOTALIN = .Rows(iLoop)("QTY") * .Rows(iLoop)("HPP")

        '                        sSQL &= "0,"
        '                        sSQL &= "0,"
        '                        sSQL &= "0,"

        '                        sTOTALOUT = 0
        '                    End If
        '                    If .Rows(iLoop)("TIPE") = "002" Then 'PR
        '                        sSQL &= "0,"
        '                        sSQL &= "0,"
        '                        sSQL &= "0,"

        '                        sTOTALIN = 0

        '                        sSQL &= "" & .Rows(iLoop)("QTY") & ","
        '                        sSQL &= "" & .Rows(iLoop)("HPP") & ","
        '                        sSQL &= "" & .Rows(iLoop)("QTY") * .Rows(iLoop)("HPP") & ","

        '                        sTOTALOUT = .Rows(iLoop)("QTY") * .Rows(iLoop)("HPP")
        '                    End If
        '                    If .Rows(iLoop)("TIPE") = "003" Then 'SI
        '                        sSQL &= "0,"
        '                        sSQL &= "0,"
        '                        sSQL &= "0,"

        '                        sTOTALIN = 0

        '                        sSQL &= "" & .Rows(iLoop)("QTY") & ","
        '                        sSQL &= "" & Average & ","
        '                        sSQL &= "" & .Rows(iLoop)("QTY") * Average & ","

        '                        sTOTALOUT = .Rows(iLoop)("QTY") * Average
        '                    End If
        '                    If .Rows(iLoop)("TIPE") = "004" Then 'SR
        '                        sSQL &= "" & .Rows(iLoop)("QTY") & ","
        '                        sSQL &= "" & Average & ","
        '                        sSQL &= "" & .Rows(iLoop)("QTY") * Average & ","

        '                        sTOTALIN = .Rows(iLoop)("QTY") * Average

        '                        sSQL &= "0,"
        '                        sSQL &= "0,"
        '                        sSQL &= "0,"

        '                        sTOTALOUT = 0
        '                    End If
        '                    If .Rows(iLoop)("TIPE") = "000" Then 'OPNAME
        '                        If .Rows(iLoop)("QTY") > 0 Then
        '                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
        '                            sSQL &= "" & Average & ","
        '                            sSQL &= "" & .Rows(iLoop)("QTY") * Average & ","

        '                            sTOTALIN = .Rows(iLoop)("QTY") * Average

        '                            sSQL &= "0,"
        '                            sSQL &= "0,"
        '                            sSQL &= "0,"

        '                            sTOTALOUT = 0
        '                        Else
        '                            sSQL &= "0,"
        '                            sSQL &= "0,"
        '                            sSQL &= "0,"

        '                            sTOTALIN = 0

        '                            sSQL &= "" & Math.Abs(CDec(.Rows(iLoop)("QTY"))) & ","
        '                            sSQL &= "" & Average & ","
        '                            sSQL &= "" & Math.Abs(CDec(.Rows(iLoop)("QTY"))) * Average & ","

        '                            sTOTALOUT = Math.Abs(CDec(.Rows(iLoop)("QTY"))) * Average
        '                        End If
        '                    End If
        '                    If .Rows(iLoop)("TIPE") = "001" Or .Rows(iLoop)("TIPE") = "004" Or (.Rows(iLoop)("TIPE") = "000" And .Rows(iLoop)("QTY") > 0) Then
        '                        QtyBalance += Math.Abs(CDbl(.Rows(iLoop)("QTY")))
        '                        TotalBalance += sTOTALIN
        '                    Else
        '                        If .Rows(iLoop)("TIPE") = "003" And CDbl(.Rows(iLoop)("QTY")) < 1 Then
        '                            QtyBalance += Math.Abs(CDbl(.Rows(iLoop)("QTY")))
        '                        Else

        '                            QtyBalance -= Math.Abs(CDbl(.Rows(iLoop)("QTY")))
        '                        End If

        '                        TotalBalance -= sTOTALOUT
        '                    End If

        '                    If QtyBalance <= 0 Then
        '                        PriceBalance = Average
        '                    Else
        '                        PriceBalance = TotalBalance / QtyBalance
        '                    End If

        '                    sSQL &= "" & QtyBalance & ","
        '                    sSQL &= "" & PriceBalance & ","
        '                    sSQL &= "" & TotalBalance & ","
        '                    sSQL &= "" & sSeq & ","
        '                    sSQL &= "" & .Rows(iLoop)("SEQREF") & ""
        '                    sSQL &= ")"

        '                    Average = PriceBalance

        '                    sSeq += 1
        '                End With

        '                Dim oConnEx As New SqlConnection
        '                Dim oCommEx As New SqlCommand
        '                Dim readerEx As SqlDataReader

        '                oConnEx = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
        '                If oConnEx.State = ConnectionState.Closed Then
        '                    oConnEx.Open()
        '                End If

        '                oCommEx.Connection = oConnEx
        '                oCommEx.CommandText = sSQL
        '                oCommEx.CommandTimeout = 120
        '                oCommEx.CommandType = CommandType.Text

        '                readerEx = oCommEx.ExecuteReader()

        '                oConnEx.Close()
        '            Next

        '            oConn = Nothing
        '            da = Nothing
        '            ds = Nothing
        '            SQL = Nothing
        '        Next

        '        PostingAverage = True
        '    Catch ex As Exception
        '        PostingAverage = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    Finally
        '        Finalize()
        '    End Try
        'End Function
        Public Function PostingAverageItem(ByVal sKDITEM As String, ByVal sKDWAREHOUSE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    PostingAverageItem = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "POSTINGAVERAGE"

                Dim Average As Decimal = 0
                Dim QtyBalance As Decimal = 0
                Dim Price As Decimal = 0
                Dim sSeq = 1
                Dim sItem = sKDITEM
                Dim StringKarakter As String = "'"
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String

                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT * FROM ("
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDPI "
                SQL &= ",NOREF = D.NAME_DISPLAY "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",B.PRICE "
                SQL &= ",TIPE = '001' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM P_PI_H A "
                SQL &= "INNER JOIN P_PI_D B "
                SQL &= "ON B.KDPI = A.KDPI "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "INNER JOIN M_VENDOR D "
                SQL &= "ON A.KDVENDOR = D.KDVENDOR "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDPR "
                SQL &= ",NOREF = D.NAME_DISPLAY "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",B.PRICE "
                SQL &= ",TIPE = '002' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM P_PR_H A "
                SQL &= "INNER JOIN P_PR_D B "
                SQL &= "ON B.KDPR = A.KDPR "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "INNER JOIN M_VENDOR D "
                SQL &= "ON A.KDVENDOR = D.KDVENDOR "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDMUTATION "
                SQL &= ",NOREF = 'MUTASI KE ' + D.NAME_DISPLAY "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",PRICE = 0 "
                SQL &= ",TIPE = '003' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM I_MUTATION_H A "
                SQL &= "INNER JOIN I_MUTATION_D B "
                SQL &= "ON B.KDMUTATION = A.KDMUTATION "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "INNER JOIN M_WAREHOUSE D "
                SQL &= "ON A.KDWAREHOUSETO = D.KDWAREHOUSE "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSEFROM = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDTERIMA "
                SQL &= ",NOREF = 'MUTASI DARI ' + ISNULL((SELECT BB.NAME_DISPLAY FROM I_MUTATION_H AA INNER JOIN M_WAREHOUSE BB ON AA.KDWAREHOUSEFROM = BB.KDWAREHOUSE WHERE AA.KDMUTATION = A.KDMUTATION), '-') "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",PRICE = 0 "
                SQL &= ",TIPE = '004' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM I_TERIMA_H A "
                SQL &= "INNER JOIN I_TERIMA_D B "
                SQL &= "ON B.KDTERIMA = A.KDTERIMA "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDSOTRANSAKSI "
                SQL &= ",NOREF = (SELECT CC.NAME_DISPLAY FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN INNER JOIN M_CUSTOMER CC ON BB.KDCUSTOMER = CC.KDCUSTOMER WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN) "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",PRICE = 0 "
                SQL &= ",TIPE = '005' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM S_SO_TRANSAKSI_H A "
                SQL &= "INNER JOIN S_SO_TRANSAKSI_D B "
                SQL &= "ON B.KDSOTRANSAKSI = A.KDSOTRANSAKSI "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDSOTANPARESEP "
                SQL &= ",NOREF = A.NAMAPASIEN "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",PRICE = 0 "
                SQL &= ",TIPE = '006' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM S_SO_TANPARESEP_H A "
                SQL &= "INNER JOIN S_SO_TANPARESEP_D B "
                SQL &= "ON B.KDSOTANPARESEP = A.KDSOTANPARESEP "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDOPNAME "
                SQL &= ",NOREF = D.NAME_DISPLAY "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",PRICE = 0 "
                SQL &= ",TIPE = '007' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM I_OPNAME_H A "
                SQL &= "INNER JOIN I_OPNAME_D B "
                SQL &= "ON B.KDOPNAME = A.KDOPNAME "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "INNER JOIN M_WAREHOUSE D "
                SQL &= "ON A.KDWAREHOUSE = D.KDWAREHOUSE "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= "UNION "
                SQL &= "(SELECT "
                SQL &= "NOID = A.KDADJUSTMENT "
                SQL &= ",NOREF = D.NAME_DISPLAY "
                SQL &= ",[DATE] = A.DATE "
                SQL &= ",KDITEM = B.KDITEM "
                SQL &= ",KDUOM = B.KDUOM "
                SQL &= ",QTY = B.QTY * C.RATE "
                SQL &= ",PRICE = 0 "
                SQL &= ",TIPE = '008' "
                SQL &= ",SEQREF = B.SEQ "
                SQL &= "FROM I_ADJUSTMENT_H A "
                SQL &= "INNER JOIN I_ADJUSTMENT_D B "
                SQL &= "ON B.KDADJUSTMENT = A.KDADJUSTMENT "
                SQL &= "INNER JOIN M_ITEM_UOM C "
                SQL &= "ON B.KDITEM = C.KDITEM AND B.KDUOM = C.KDUOM "
                SQL &= "INNER JOIN M_WAREHOUSE D "
                SQL &= "ON A.KDWAREHOUSE = D.KDWAREHOUSE "
                SQL &= "WHERE B.KDITEM = '" & sKDITEM & "'"
                SQL &= "AND A.KDWAREHOUSE = '" & sKDWAREHOUSE & "' "
                SQL &= "AND C.RATE = 1 "
                SQL &= ") "
                SQL &= ") AS A "
                SQL &= "ORDER BY CONVERT(VARCHAR(10), [DATE], 112), TIPE, SEQREF  "

                Try
                    Dim dsClear = GetData(sItem)
                    oConnection.db.A_STOCK_CARDs.DeleteAllOnSubmit(dsClear)
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")

                If ds.Tables("ALL").Rows.Count < 1 Then
                    PostingAverageItem = False
                    Exit Function
                End If

                For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    Dim sSQL = "INSERT INTO A_STOCK_CARD VALUES "

                    With ds.Tables("ALL")
                        sSQL &= "("
                        sSQL &= "'" & .Rows(iLoop)("NOID") & "',"
                        sSQL &= "'" & CDate(.Rows(iLoop)("DATE")).ToString("yyyy-MM-dd HH:mm:ss") & "',"
                        sSQL &= "'" & .Rows(iLoop)("KDITEM") & "',"
                        sSQL &= "'" & .Rows(iLoop)("KDUOM") & "',"

                        If .Rows(iLoop)("TIPE") = "001" Then 'PI
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            sSQL &= "0,"
                            QtyBalance += Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        If .Rows(iLoop)("TIPE") = "002" Then 'PR
                            sSQL &= "0,"
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            QtyBalance -= Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        If .Rows(iLoop)("TIPE") = "003" Then 'MUTASI KELUAR
                            sSQL &= "0,"
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            QtyBalance -= Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        If .Rows(iLoop)("TIPE") = "004" Then 'MUTASI MASUK
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            sSQL &= "0,"
                            QtyBalance += Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        If .Rows(iLoop)("TIPE") = "005" Then 'RESEP
                            sSQL &= "0,"
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            QtyBalance -= Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        If .Rows(iLoop)("TIPE") = "006" Then 'TANPA RESEP
                            sSQL &= "0,"
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            QtyBalance -= Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        If .Rows(iLoop)("TIPE") = "007" Then 'OPNAME
                            If .Rows(iLoop)("QTY") > 0 Then
                                sSQL &= "" & .Rows(iLoop)("QTY") & ","
                                sSQL &= "0,"
                                QtyBalance += Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                            Else
                                sSQL &= "0,"
                                sSQL &= "" & .Rows(iLoop)("QTY") & ","
                                QtyBalance -= Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                            End If
                        End If
                        If .Rows(iLoop)("TIPE") = "008" Then 'PI
                            sSQL &= "" & .Rows(iLoop)("QTY") & ","
                            sSQL &= "0,"
                            QtyBalance += Math.Abs(CDbl(.Rows(iLoop)("QTY")))
                        End If
                        sSQL &= "" & QtyBalance & ","
                        sSQL &= "" & .Rows(iLoop)("PRICE") & ","
                        sSQL &= "'" & .Rows(iLoop)("NOREF") & "',"
                        sSQL &= "" & sSeq & ","
                        sSQL &= "" & .Rows(iLoop)("SEQREF") & ""
                        sSQL &= ")"

                        sSeq += 1
                    End With

                    Dim oConnEx As New SqlConnection
                    Dim oCommEx As New SqlCommand
                    Dim readerEx As SqlDataReader

                    oConnEx = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                    If oConnEx.State = ConnectionState.Closed Then
                        oConnEx.Open()
                    End If

                    oCommEx.Connection = oConnEx
                    oCommEx.CommandText = sSQL
                    oCommEx.CommandTimeout = 120
                    oCommEx.CommandType = CommandType.Text

                    readerEx = oCommEx.ExecuteReader()

                    oConnEx.Close()
                Next

                oConn = Nothing
                da = Nothing
                ds = Nothing
                SQL = Nothing

                PostingAverageItem = True
            Catch ex As Exception
                PostingAverageItem = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                Finalize()
            End Try
        End Function
        Public Function InsertData(ByVal entityDetail As List(Of A_STOCK_CARD_NEW)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = "STOCK CARD"
                sSTATUS = "INSERT"

                Try
                    oConnection.db.A_STOCK_CARD_NEWs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function PostingAverageItemNew(ByVal sKDITEM As String, ByVal sKDUOM As String, ByVal sKDWAREHOUSE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    PostingAverageItemNew = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "POSTINGAVERAGE"

                'Dim Average As Decimal = 0
                'Dim QtyBalance As Decimal = 0
                'Dim Price As Decimal = 0
                'Dim sItem = sKDITEM
                'Dim StringKarakter As String = "'"
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String

                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "EXEC R_KARTUSTOK_GUDANG @KDWAREHOUSE = '" & sKDWAREHOUSE & "', @KDITEM = '" & sKDITEM & "', @KDUOM = '" & sKDUOM & "'  "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")

                If DeleteData(sKDITEM) = False Then
                    PostingAverageItemNew = False
                    Exit Function
                End If

                If ds.Tables("ALL").Rows.Count < 1 Then
                    PostingAverageItemNew = False
                    Exit Function
                End If

                ' ***** DETIL *****
                Dim arrDetail = GetStructureDetailList()
                Dim sSISA As Decimal = 0
                Dim sSEQ As Integer = 0

                For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    With ds.Tables("ALL")
                        If sSISA = 0 Then
                            sSISA = .Rows(iLoop)("TOTAL")
                        Else
                            sSISA += .Rows(iLoop)("TOTAL")
                        End If

                        Dim sDATE As DateTime = .Rows(iLoop)("TANGGAL")
                        Dim sNOREFERENCE As String = .Rows(iLoop)("NOFAKTUR")
                        Dim sURAIAN As String = .Rows(iLoop)("URAIAN")
                        Dim sMASUK As Decimal = 0
                        Dim sKELUAR As Decimal = 0
                        Dim sPRICE As Decimal = .Rows(iLoop)("HARGA")
                        Dim sEXPIRE As String = .Rows(iLoop)("TANGGALEXPIRE")
                        Dim sKETERANGAN As String = .Rows(iLoop)("KETRANGAN")
                        Dim sSEQREF As Integer = .Rows(iLoop)("SEQ")

                        If .Rows(iLoop)("TOTAL") >= 0 Then
                            sMASUK = .Rows(iLoop)("TOTAL")
                        Else
                            sKELUAR = - .Rows(iLoop)("TOTAL")
                        End If

                        Dim dsDetail = GetStructureDetail()
                        With dsDetail
                            .DATE = sDATE
                            .NOREFERENCE = sNOREFERENCE
                            .BATCH = ""
                            .URAIAN = sURAIAN
                            .MASUK = sMASUK
                            .KELUAR = sKELUAR
                            .SISA = sSISA
                            .PRICE = sPRICE
                            .EXPIRE = sEXPIRE
                            .KETERANGAN = sKETERANGAN
                            .KDITEM = sKDITEM
                            .KDUOM = sKDUOM
                            .SEQ = sSEQ
                            .SEQREF = sSEQREF
                        End With

                        arrDetail.Add(dsDetail)

                        sSEQ += 1
                    End With
                Next

                PostingAverageItemNew = InsertData(arrDetail)
            Catch ex As Exception
                PostingAverageItemNew = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                Finalize()
            End Try
        End Function
        Public Function DeleteData(ByVal sKDITEM As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "DELETE"


                Dim ds = oConnection.db.A_STOCK_CARD_NEWs.Where(Function(x) x.KDITEM = sKDITEM)

                Try
                    oConnection.db.A_STOCK_CARD_NEWs.DeleteAllOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace