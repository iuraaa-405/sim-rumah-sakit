Imports System.Threading

Imports System.Data
Imports System.Data.SqlClient

Namespace Accounting
    Public Class clsJournal
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public isRecalculate As Boolean = False

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

            sMODUL = "JRL"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_JOURNAL_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_JOURNAL_H
        End Function
        Public Function GetStructureDetail() As A_JOURNAL_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New A_JOURNAL_D
        End Function
        Public Function GetStructureDetailList() As List(Of A_JOURNAL_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of A_JOURNAL_D)
        End Function
        Public Function GetData() As List(Of A_JOURNAL_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_JOURNAL_Hs.OrderByDescending(Function(x) x.KDJOURNAL).ToList()
        End Function
        Public Function GetData(ByVal sKDJOURNAL As String) As A_JOURNAL_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_JOURNAL_Hs.FirstOrDefault(Function(x) x.KDJOURNAL = sKDJOURNAL)
        End Function
        Public Function GetDataDetail() As List(Of A_JOURNAL_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.A_JOURNAL_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDJOURNAL As String) As List(Of A_JOURNAL_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.A_JOURNAL_Ds.Where(Function(x) x.KDJOURNAL = sKDJOURNAL).ToList()
        End Function
        Public Function InsertData(ByVal entity As A_JOURNAL_H, ByVal entityDetail As List(Of A_JOURNAL_D), ByVal isAuto As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJOURNAL
                sSTATUS = "INSERT"

                If isRecalculate = False Then
                    'Generate Auto Number
                    Try
                        If isAuto = False Then
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                            If sLASTNUMBER = 0 Then
                                Try
                                    oCounter.InsertData(sMODUL, entity.DATE)
                                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                                Catch ex As Exception
                                    sLASTNUMBER = 0
                                End Try
                            End If

                            entity.KDJOURNAL = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                            For Each iLoop In entityDetail
                                iLoop.KDJOURNAL = entity.KDJOURNAL
                            Next
                        End If
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    'End Generate

                    Try
                        oConnection.db.A_JOURNAL_Hs.InsertOnSubmit(entity)
                        oConnection.db.A_JOURNAL_Ds.InsertAllOnSubmit(entityDetail)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                Dim sCOABULANAN = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_MONTH

                Try
                    For Each iLoop In entityDetail
                        Dim sKDCOA = iLoop.KDCOA

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

                        SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(ds, "A_COA_BALANCE")

                        If ds.Tables("A_COA_BALANCE").Rows.Count < 1 Then
                            Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                            sSQL &= "("
                            sSQL &= "'" & entity.DATECREATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & entity.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & sKDCOA & "',"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "" & Year(entity.DATE) & ""
                            sSQL &= ")"

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
                        End If

                        Dim IsDebet = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA).DC

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA WHERE KDCOA = '" & sKDCOA & "'"

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(ds, "A_COA")

                        With ds.Tables("A_COA")
                            Select Case IsDebet
                                Case "D"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(entity.DATE) < 10, "0" & Month(entity.DATE).ToString(), Month(entity.DATE).ToString()) & " += " & iLoop.DEBIT - iLoop.CREDIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

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
                                Case "C"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(entity.DATE) < 10, "0" & Month(entity.DATE).ToString(), Month(entity.DATE).ToString()) & " += " & iLoop.CREDIT - iLoop.DEBIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

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
                            End Select

                            If .Rows(0)("GROUPS") = "PENDAPATAN" Or .Rows(0)("GROUPS") = "PENGELUARAN" Or .Rows(0)("GROUPS") = "LAIN - LAIN" Then

                                SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(entity.DATE)

                                oComm.Connection = oConn
                                oComm.CommandText = SQL
                                oComm.CommandTimeout = 120
                                oComm.CommandType = CommandType.Text

                                da = New SqlDataAdapter(oComm)
                                da.Fill(ds, "A_COA_BULANAN")

                                If ds.Tables("A_COA_BULANAN").Rows.Count < 1 Then
                                    Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                                    sSQL &= "("
                                    sSQL &= "'" & entity.DATECREATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & entity.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & sCOABULANAN & "',"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "" & Year(entity.DATE) & ""
                                    sSQL &= ")"

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
                                End If

                                Dim oConnExLaba As New SqlConnection
                                Dim oCommExLaba As New SqlCommand
                                Dim readerExLaba As SqlDataReader

                                oConnExLaba = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                                If oConnExLaba.State = ConnectionState.Closed Then
                                    oConnExLaba.Open()
                                End If

                                oCommExLaba.Connection = oConnExLaba
                                oCommExLaba.CommandText = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(entity.DATE) < 10, "0" & Month(entity.DATE).ToString(), Month(entity.DATE).ToString()) & " += " & iLoop.CREDIT - iLoop.DEBIT & " WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(entity.DATE)
                                oCommExLaba.CommandTimeout = 120
                                oCommExLaba.CommandType = CommandType.Text

                                readerExLaba = oCommExLaba.ExecuteReader()

                                oConnExLaba.Close()
                            End If
                        End With
                    Next
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

                If isRecalculate = False Then
                    Try
                        If isAuto = False Then
                            'Update Last Number
                            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                            'End Update
                        End If
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As A_JOURNAL_H, ByVal entityDetail As List(Of A_JOURNAL_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJOURNAL
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.A_JOURNAL_Hs.FirstOrDefault(Function(x) x.KDJOURNAL = entity.KDJOURNAL)

                Try
                    oConnection.db.A_JOURNAL_Hs.DeleteOnSubmit(ds)
                    oConnection.db.A_JOURNAL_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.A_JOURNAL_Ds.Where(Function(x) x.KDJOURNAL = entity.KDJOURNAL)

                Try
                    oConnection.db.A_JOURNAL_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.A_JOURNAL_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim sCOABULANAN = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_MONTH

                Try
                    For Each iLoop In dsDetail
                        Dim sKDCOA = iLoop.KDCOA

                        Dim oConn As New SqlConnection
                        Dim oComm As New SqlCommand
                        Dim da As SqlDataAdapter
                        Dim dSet As New DataSet
                        Dim SQL As String

                        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(dSet, "A_COA_BALANCE")

                        If dSet.Tables("A_COA_BALANCE").Rows.Count < 1 Then
                            Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                            sSQL &= "("
                            sSQL &= "'" & entity.DATECREATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & entity.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & sKDCOA & "',"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "" & Year(ds.DATE) & ""
                            sSQL &= ")"

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
                        End If

                        Dim IsDebet = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA).DC

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA WHERE KDCOA = '" & sKDCOA & "'"

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(dSet, "A_COA")

                        With dSet.Tables("A_COA")
                            Select Case IsDebet
                                Case "C"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(ds.DATE) < 10, "0" & Month(ds.DATE).ToString(), Month(ds.DATE).ToString()) & " += " & iLoop.DEBIT - iLoop.CREDIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

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
                                Case "D"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(ds.DATE) < 10, "0" & Month(ds.DATE).ToString(), Month(ds.DATE).ToString()) & " += " & iLoop.CREDIT - iLoop.DEBIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

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
                            End Select

                            If .Rows(0)("GROUPS") = "PENDAPATAN" Or .Rows(0)("GROUPS") = "PENGELUARAN" Or .Rows(0)("GROUPS") = "LAIN - LAIN" Then

                                SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(entity.DATE)

                                oComm.Connection = oConn
                                oComm.CommandText = SQL
                                oComm.CommandTimeout = 120
                                oComm.CommandType = CommandType.Text

                                da = New SqlDataAdapter(oComm)
                                da.Fill(dSet, "A_COA_BULANAN")

                                If dSet.Tables("A_COA_BULANAN").Rows.Count < 1 Then
                                    Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                                    sSQL &= "("
                                    sSQL &= "'" & entity.DATECREATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & entity.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & sCOABULANAN & "',"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "" & Year(ds.DATE) & ""
                                    sSQL &= ")"

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
                                End If

                                Dim oConnExLaba As New SqlConnection
                                Dim oCommExLaba As New SqlCommand
                                Dim readerExLaba As SqlDataReader

                                oConnExLaba = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                                If oConnExLaba.State = ConnectionState.Closed Then
                                    oConnExLaba.Open()
                                End If

                                oCommExLaba.Connection = oConnExLaba
                                oCommExLaba.CommandText = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(ds.DATE) < 10, "0" & Month(ds.DATE).ToString(), Month(ds.DATE).ToString()) & " += " & iLoop.DEBIT - iLoop.CREDIT & " WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(entity.DATE)
                                oCommExLaba.CommandTimeout = 120
                                oCommExLaba.CommandType = CommandType.Text

                                readerExLaba = oCommExLaba.ExecuteReader()

                                oConnExLaba.Close()
                            End If
                        End With
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    For Each iLoop In entityDetail
                        Dim sKDCOA = iLoop.KDCOA

                        Dim oConn As New SqlConnection
                        Dim oComm As New SqlCommand
                        Dim da As SqlDataAdapter
                        Dim dSet As New DataSet
                        Dim SQL As String

                        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(dSet, "A_COA_BALANCE")

                        If dSet.Tables("A_COA_BALANCE").Rows.Count < 1 Then
                            Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                            sSQL &= "("
                            sSQL &= "'" & entity.DATECREATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & entity.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & sKDCOA & "',"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "" & Year(entity.DATE) & ""
                            sSQL &= ")"

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
                        End If

                        Dim IsDebet = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA).DC

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA WHERE KDCOA = '" & sKDCOA & "'"

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(dSet, "A_COA")

                        With dSet.Tables("A_COA")
                            Select Case IsDebet
                                Case "D"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(entity.DATE) < 10, "0" & Month(entity.DATE).ToString(), Month(entity.DATE).ToString()) & " += " & iLoop.DEBIT - iLoop.CREDIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

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
                                Case "C"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(entity.DATE) < 10, "0" & Month(entity.DATE).ToString(), Month(entity.DATE).ToString()) & " += " & iLoop.CREDIT - iLoop.DEBIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(entity.DATE)

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
                            End Select

                            If .Rows(0)("GROUPS") = "PENDAPATAN" Or .Rows(0)("GROUPS") = "PENGELUARAN" Or .Rows(0)("GROUPS") = "LAIN - LAIN" Then

                                SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(entity.DATE)

                                oComm.Connection = oConn
                                oComm.CommandText = SQL
                                oComm.CommandTimeout = 120
                                oComm.CommandType = CommandType.Text

                                da = New SqlDataAdapter(oComm)
                                da.Fill(dSet, "A_COA_BULANAN")

                                If dSet.Tables("A_COA_BULANAN").Rows.Count < 1 Then
                                    Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                                    sSQL &= "("
                                    sSQL &= "'" & entity.DATECREATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & entity.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & sCOABULANAN & "',"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "" & Year(entity.DATE) & ""
                                    sSQL &= ")"

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
                                End If

                                Dim oConnExLaba As New SqlConnection
                                Dim oCommExLaba As New SqlCommand
                                Dim readerExLaba As SqlDataReader

                                oConnExLaba = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                                If oConnExLaba.State = ConnectionState.Closed Then
                                    oConnExLaba.Open()
                                End If

                                oCommExLaba.Connection = oConnExLaba
                                oCommExLaba.CommandText = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(entity.DATE) < 10, "0" & Month(entity.DATE).ToString(), Month(entity.DATE).ToString()) & " += " & iLoop.CREDIT - iLoop.DEBIT & " WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(entity.DATE)
                                oCommExLaba.CommandTimeout = 120
                                oCommExLaba.CommandType = CommandType.Text

                                readerExLaba = oCommExLaba.ExecuteReader()

                                oConnExLaba.Close()
                            End If
                        End With
                    Next
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

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDJOURNAL As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.A_JOURNAL_Hs.FirstOrDefault(Function(x) x.KDJOURNAL = sKDJOURNAL)
                Dim dsDetail = oConnection.db.A_JOURNAL_Ds.Where(Function(x) x.KDJOURNAL = sKDJOURNAL)

                Try
                    'ds.ISDELETE = True

                    oConnection.db.A_JOURNAL_Hs.DeleteOnSubmit(ds)
                    oConnection.db.A_JOURNAL_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim sCOABULANAN = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_MONTH

                Try
                    For Each iLoop In dsDetail
                        Dim sKDCOA = iLoop.KDCOA

                        Dim oConn As New SqlConnection
                        Dim oComm As New SqlCommand
                        Dim da As SqlDataAdapter
                        Dim dSet As New DataSet
                        Dim SQL As String

                        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(ds.DATE)

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(dSet, "A_COA_BALANCE")

                        If dSet.Tables("A_COA_BALANCE").Rows.Count < 1 Then
                            Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                            sSQL &= "("
                            sSQL &= "'" & ds.DATECREATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & ds.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                            sSQL &= "'" & sKDCOA & "',"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "0,"
                            sSQL &= "" & Year(ds.DATE) & ""
                            sSQL &= ")"

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
                        End If

                        Dim IsDebet = oConnection.db.A_COAs.FirstOrDefault(Function(x) x.KDCOA = sKDCOA).DC

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        SQL = "SELECT * FROM A_COA WHERE KDCOA = '" & sKDCOA & "'"

                        oComm.Connection = oConn
                        oComm.CommandText = SQL
                        oComm.CommandTimeout = 120
                        oComm.CommandType = CommandType.Text

                        da = New SqlDataAdapter(oComm)
                        da.Fill(dSet, "A_COA")

                        With dSet.Tables("A_COA")
                            Select Case IsDebet
                                Case "C"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(ds.DATE) < 10, "0" & Month(ds.DATE).ToString(), Month(ds.DATE).ToString()) & " += " & iLoop.DEBIT - iLoop.CREDIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(ds.DATE)

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
                                Case "D"
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(ds.DATE) < 10, "0" & Month(ds.DATE).ToString(), Month(ds.DATE).ToString()) & " += " & iLoop.CREDIT - iLoop.DEBIT & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & Year(ds.DATE)

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
                            End Select

                            If .Rows(0)("GROUPS") = "PENDAPATAN" Or .Rows(0)("GROUPS") = "PENGELUARAN" Or .Rows(0)("GROUPS") = "LAIN - LAIN" Then

                                SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(ds.DATE)

                                oComm.Connection = oConn
                                oComm.CommandText = SQL
                                oComm.CommandTimeout = 120
                                oComm.CommandType = CommandType.Text

                                da = New SqlDataAdapter(oComm)
                                da.Fill(dSet, "A_COA_BULANAN")

                                If dSet.Tables("A_COA_BULANAN").Rows.Count < 1 Then
                                    Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                                    sSQL &= "("
                                    sSQL &= "'" & ds.DATECREATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & ds.DATEUPDATED.ToString("yyyy-MM-dd") & "',"
                                    sSQL &= "'" & sCOABULANAN & "',"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "0,"
                                    sSQL &= "" & Year(ds.DATE) & ""
                                    sSQL &= ")"

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
                                End If

                                Dim oConnExLaba As New SqlConnection
                                Dim oCommExLaba As New SqlCommand
                                Dim readerExLaba As SqlDataReader

                                oConnExLaba = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                                If oConnExLaba.State = ConnectionState.Closed Then
                                    oConnExLaba.Open()
                                End If

                                oCommExLaba.Connection = oConnExLaba
                                oCommExLaba.CommandText = "UPDATE A_COA_BALANCE SET SALDO" & IIf(Month(ds.DATE) < 10, "0" & Month(ds.DATE).ToString(), Month(ds.DATE).ToString()) & " += " & iLoop.DEBIT - iLoop.CREDIT & " WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & Year(ds.DATE)
                                oCommExLaba.CommandTimeout = 120
                                oCommExLaba.CommandType = CommandType.Text

                                readerExLaba = oCommExLaba.ExecuteReader()

                                oConnExLaba.Close()
                            End If
                        End With
                    Next
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
        Public Function RecalculateData() As Boolean
            Try
                If Not oConnection.GetConnection Then
                    RecalculateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.A_COA_BALANCEs

                Try
                    oConnection.db.A_COA_BALANCEs.DeleteAllOnSubmit(ds)
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim sCOABULANAN = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_MONTH
                Dim dsDetail = oConnection.db.A_COAs.Where(Function(x) x.TYPE = 1)

                Dim sFirstYear = oConnection.db.A_JOURNAL_Hs.OrderBy(Function(x) x.DATE).FirstOrDefault().DATE.Year

                Try
                    For Each iLoop In dsDetail
                        Dim sKDCOA = iLoop.KDCOA

                        Dim oConn As New SqlConnection
                        Dim oComm As New SqlCommand
                        Dim da As SqlDataAdapter
                        Dim dSet As New DataSet
                        Dim SQL As String

                        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                        oConn = New SqlConnection(sConn)
                        If oConn.State = ConnectionState.Closed Then
                            oConn.Open()
                        End If

                        For xLoop As Integer = 2012 To Now.Year
                            dSet = New DataSet

                            SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & xLoop

                            oComm.Connection = oConn
                            oComm.CommandText = SQL
                            oComm.CommandTimeout = 120
                            oComm.CommandType = CommandType.Text

                            da = New SqlDataAdapter(oComm)
                            da.Fill(dSet, "A_COA_BALANCE")

                            If dSet.Tables("A_COA_BALANCE").Rows.Count < 1 Then
                                Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                                sSQL &= "("
                                sSQL &= "'" & Now.ToString("yyyy-MM-dd") & "',"
                                sSQL &= "'" & Now.ToString("yyyy-MM-dd") & "',"
                                sSQL &= "'" & sKDCOA & "',"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "0,"
                                sSQL &= "" & xLoop & ""
                                sSQL &= ")"

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
                            End If

                            Dim IsDebet = iLoop.DC

                            oConn = New SqlConnection(sConn)
                            If oConn.State = ConnectionState.Closed Then
                                oConn.Open()
                            End If

                            For vLoop As Integer = 1 To 12
                                dSet = New DataSet

                                If iLoop.DC = "D" Then
                                    SQL = "SELECT KDCOA, TOTAL = SUM(DEBIT) - SUM(CREDIT) FROM A_JOURNAL_D AS A INNER JOIN A_JOURNAL_H AS B ON A.KDJOURNAL = B.KDJOURNAL WHERE KDCOA = '" & sKDCOA & "' AND YEAR(DATE) = " & xLoop & " AND MONTH(DATE) = " & vLoop & " GROUP BY KDCOA"
                                Else
                                    SQL = "SELECT KDCOA, TOTAL = SUM(CREDIT) - SUM(DEBIT) FROM A_JOURNAL_D AS A INNER JOIN A_JOURNAL_H AS B ON A.KDJOURNAL = B.KDJOURNAL WHERE KDCOA = '" & sKDCOA & "' AND YEAR(DATE) = " & xLoop & " AND MONTH(DATE) = " & vLoop & " GROUP BY KDCOA"
                                End If

                                oComm.Connection = oConn
                                oComm.CommandText = SQL
                                oComm.CommandTimeout = 120
                                oComm.CommandType = CommandType.Text

                                da = New SqlDataAdapter(oComm)
                                da.Fill(dSet, "A_JOURNAL")

                                If dSet.Tables("A_JOURNAL").Rows.Count < 1 Then
                                    Continue For
                                End If

                                With dSet.Tables("A_JOURNAL")
                                    Dim sSQL = "UPDATE A_COA_BALANCE SET SALDO" & IIf(vLoop < 10, "0" & vLoop, vLoop) & " = " & .Rows(0)("TOTAL") & " WHERE KDCOA = '" & sKDCOA & "' AND [YEAR] = " & xLoop

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
                                End With
                            Next

                            If iLoop.GROUPS = "PENDAPATAN" Or iLoop.GROUPS = "PENGELUARAN" Or iLoop.GROUPS = "LAIN - LAIN" Then
                                dSet = New DataSet

                                For vLoop As Integer = 1 To 12
                                    dSet = New DataSet

                                    If iLoop.DC = "D" Then
                                        SQL = "SELECT KDCOA, TOTAL = SUM(DEBIT) - SUM(CREDIT) FROM A_JOURNAL_D AS A INNER JOIN A_JOURNAL_H AS B ON A.KDJOURNAL = B.KDJOURNAL WHERE KDCOA = '" & sKDCOA & "' AND YEAR(DATE) = " & xLoop & " AND MONTH(DATE) = " & vLoop & " GROUP BY KDCOA"
                                    Else
                                        SQL = "SELECT KDCOA, TOTAL = SUM(CREDIT) - SUM(DEBIT) FROM A_JOURNAL_D AS A INNER JOIN A_JOURNAL_H AS B ON A.KDJOURNAL = B.KDJOURNAL WHERE KDCOA = '" & sKDCOA & "' AND YEAR(DATE) = " & xLoop & " AND MONTH(DATE) = " & vLoop & " GROUP BY KDCOA"
                                    End If

                                    oComm.Connection = oConn
                                    oComm.CommandText = SQL
                                    oComm.CommandTimeout = 120
                                    oComm.CommandType = CommandType.Text

                                    da = New SqlDataAdapter(oComm)
                                    da.Fill(dSet, "A_JOURNAL")

                                    If dSet.Tables("A_JOURNAL").Rows.Count < 1 Then
                                        Continue For
                                    End If

                                    With dSet.Tables("A_JOURNAL")
                                        If iLoop.GROUPS = "PENDAPATAN" Or iLoop.GROUPS = "PENGELUARAN" Or iLoop.GROUPS = "LAIN - LAIN" Then
                                            SQL = "SELECT * FROM A_COA_BALANCE WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & xLoop

                                            oComm.Connection = oConn
                                            oComm.CommandText = SQL
                                            oComm.CommandTimeout = 120
                                            oComm.CommandType = CommandType.Text

                                            da = New SqlDataAdapter(oComm)
                                            da.Fill(dSet, "A_COA_BULANAN")

                                            If dSet.Tables("A_COA_BULANAN").Rows.Count < 1 Then
                                                Dim sSQL = "INSERT INTO A_COA_BALANCE VALUES "
                                                sSQL &= "("
                                                sSQL &= "'" & Now.ToString("yyyy-MM-dd") & "',"
                                                sSQL &= "'" & Now.ToString("yyyy-MM-dd") & "',"
                                                sSQL &= "'" & sCOABULANAN & "',"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "0,"
                                                sSQL &= "" & xLoop & ""
                                                sSQL &= ")"

                                                Dim oConnExBulanan As New SqlConnection
                                                Dim oCommExBulanan As New SqlCommand
                                                Dim readerExBulanan As SqlDataReader

                                                oConnExBulanan = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                                                If oConnExBulanan.State = ConnectionState.Closed Then
                                                    oConnExBulanan.Open()
                                                End If

                                                oCommExBulanan.Connection = oConnExBulanan
                                                oCommExBulanan.CommandText = sSQL
                                                oCommExBulanan.CommandTimeout = 120
                                                oCommExBulanan.CommandType = CommandType.Text

                                                readerExBulanan = oCommExBulanan.ExecuteReader()

                                                oConnExBulanan.Close()
                                            End If

                                            Dim oConnExLaba As New SqlConnection
                                            Dim oCommExLaba As New SqlCommand
                                            Dim readerExLaba As SqlDataReader

                                            oConnExLaba = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))
                                            If oConnExLaba.State = ConnectionState.Closed Then
                                                oConnExLaba.Open()
                                            End If

                                            oCommExLaba.Connection = oConnExLaba

                                            If iLoop.DC = "D" Then
                                                oCommExLaba.CommandText = "UPDATE A_COA_BALANCE SET SALDO" & IIf(vLoop < 10, "0" & vLoop, vLoop) & " -= " & .Rows(0)("TOTAL") & " WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & xLoop
                                            Else
                                                oCommExLaba.CommandText = "UPDATE A_COA_BALANCE SET SALDO" & IIf(vLoop < 10, "0" & vLoop, vLoop) & " += " & .Rows(0)("TOTAL") & " WHERE KDCOA = '" & sCOABULANAN & "' AND [YEAR] = " & xLoop
                                            End If

                                            oCommExLaba.CommandTimeout = 120
                                            oCommExLaba.CommandType = CommandType.Text

                                            readerExLaba = oCommExLaba.ExecuteReader()

                                            oConnExLaba.Close()
                                        End If
                                    End With
                                Next
                            End If
                        Next
                    Next
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

                RecalculateData = True
            Catch ex As Exception
                RecalculateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataFix = False
                    Exit Function
                End If

                RecalculateData()

                'Dim entity = oConnection.db.A_JOURNAL_Hs

                'For Each iLoop In entity
                '    isRecalculate = True

                '    Dim sKDJOURNAL = iLoop.KDJOURNAL
                '    Dim entityDetail = oConnection.db.A_JOURNAL_Ds.Where(Function(x) x.KDJOURNAL = sKDJOURNAL)

                '    Try
                '        If isNew = True Then
                '            InsertData(iLoop, entityDetail.ToList, False)
                '        Else
                '            UpdateData(iLoop, entityDetail.ToList)
                '        End If
                '    Catch ex As Exception
                '        MsgBox(ex)
                '    End Try

                '    Thread.Sleep(100)
                'Next

                UpdateDataFix = True
            Catch ex As Exception
                UpdateDataFix = False
                MsgBox(ex)
            End Try
        End Function
    End Class
End Namespace