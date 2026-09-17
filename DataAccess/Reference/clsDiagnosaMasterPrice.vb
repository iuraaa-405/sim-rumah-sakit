
Namespace Reference
    Public Class clsDiagnosaMasterPrice
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "DPMP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_DIAGNOSA_MASTER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_DIAGNOSA_MASTER
        End Function
        Public Function GetData() As List(Of R_DIAGNOSA_MASTER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_DIAGNOSA_MASTERs.OrderByDescending(Function(x) x.KDDIAGNOSAMASTER).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As R_DIAGNOSA_MASTER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_DIAGNOSA_MASTERs.FirstOrDefault(Function(x) x.KDDIAGNOSAMASTER = Parameter)
        End Function
        Public Function GetDataByKD(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataByKD = Nothing
                Exit Function
            End If
            GetDataByKD = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As R_DIAGNOSA_MASTER) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDDIAGNOSAMASTER
                sSTATUS = "INSERT"
                Dim sKDDIAGNOSAMASTER As String = String.Empty

                Try
                    If entity.KDDIAGNOSAMASTER = String.Empty Then
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATECREATED)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDDIAGNOSAMASTER = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                        Try
                            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try
                    End If

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.R_DIAGNOSA_MASTERs.InsertOnSubmit(entity)
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

                InsertData = entity.KDDIAGNOSAMASTER
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_DIAGNOSA_MASTER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDIAGNOSAMASTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_DIAGNOSA_MASTERs.FirstOrDefault(Function(x) x.KDDIAGNOSAMASTER = entity.KDDIAGNOSAMASTER)

                Try
                    oConnection.db.R_DIAGNOSA_MASTERs.DeleteOnSubmit(ds)
                    oConnection.db.R_DIAGNOSA_MASTERs.InsertOnSubmit(entity)

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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.R_DIAGNOSA_MASTERs.FirstOrDefault(Function(x) x.KDDIAGNOSAMASTER.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.R_DIAGNOSA_MASTERs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

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
        Public Function DIAGNOSA_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    DIAGNOSA_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DIAGNOSAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    DIAGNOSA_Default = ds.KDDIAGNOSA
                Else
                    DIAGNOSA_Default = String.Empty
                End If
            Catch ex As Exception
                DIAGNOSA_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PROSEDUR_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    PROSEDUR_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PROSEDURs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    PROSEDUR_Default = ds.KDPROSEDUR
                Else
                    PROSEDUR_Default = String.Empty
                End If
            Catch ex As Exception
                PROSEDUR_Default = String.Empty
                Throw ex
            End Try
        End Function

        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.R_DIAGNOSA_MASTERs

        '        For Each iLoop In entity
        '            Dim sKDDIAGNOSAMASTER = iLoop.KDDIAGNOSAMASTER
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDDIAGNOSAMASTER = sKDDIAGNOSAMASTER And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDDIAGNOSAMASTER = sKDDIAGNOSAMASTER And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace