Imports DataAccess.My.Resources
Imports DataAccess

Namespace Digital
    Public Class clsR_Order
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter
            End If

            sMODUL = "ORDER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_ORDER
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_ORDER
        End Function
        Public Function GetStructureHeaderFarmasi() As R_ORDER_ANTRIANFARMASI
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeaderFarmasi = Nothing
            End If
            GetStructureHeaderFarmasi = New R_ORDER_ANTRIANFARMASI
        End Function
        Public Function GetData(ByVal sKDORDER As String) As R_ORDER
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
        End Function
        Public Function GetDataAntrianFarmasi(ByVal sKDORDER As String) As R_ORDER_ANTRIANFARMASI
            If Not oConnection.GetConnectionRME() Then
                GetDataAntrianFarmasi = Nothing
                Exit Function
            End If
            GetDataAntrianFarmasi = oConnection.dbRME.R_ORDER_ANTRIANFARMASIs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
        End Function
        Public Function GetDataTerakhir() As R_ORDER
            If Not oConnection.GetConnectionRME() Then
                GetDataTerakhir = Nothing
                Exit Function
            End If
            GetDataTerakhir = oConnection.dbRME.R_ORDERs.OrderByDescending(Function(x) x.DATECREATED).FirstOrDefault()
        End Function
        Public Function GetDataNoReference(ByVal sNOMORREFERENCE As String, ByVal sMEMO As String) As R_ORDER
            If Not oConnection.GetConnectionRME() Then
                GetDataNoReference = Nothing
                Exit Function
            End If
            GetDataNoReference = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.NOMORREFERENCE = sNOMORREFERENCE And x.MEMO = sMEMO)
        End Function
        Public Function GetDataDetailNoReference(ByVal sNOMORREFERENCE As String) As List(Of R_ORDER)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailNoReference = Nothing
                Exit Function
            End If
            GetDataDetailNoReference = oConnection.dbRME.R_ORDERs.Where(Function(x) x.NOMORREFERENCE = sNOMORREFERENCE).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_ORDER) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
                sSTATUS = "INSERT"

                sMODUL = entity.JENISORDER

                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.TANGGALORDER)

                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.TANGGALORDER)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.TANGGALORDER)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.KDORDER = sMODUL & entity.TANGGALORDER.ToString("yyyyMMdd") & (sLASTNUMBER + 1).ToString.PadLeft(3, "0")

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.TANGGALORDER), Year(entity.TANGGALORDER))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_ORDERs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDORDER

                If entity.MEMO = "ORDER FARMASI" Then
                    Try
                        Dim oSuara As New Digital.clsSuaraKeFarmasi

                        Dim dsSuara = oSuara.GetStructureHeader
                        With dsSuara
                            .DATECREATED = entity.DATECREATED
                            .DATEUPDATED = entity.DATEUPDATED
                            .KDSUARA = 0
                            If entity.NOMORREFERENCE.Contains("AMIGD") Then
                                .MEMO = "IGD"
                            Else
                                .MEMO = entity.JENISORDER
                            End If
                            .ISCHEKED = False
                            .KDUSER = "INSERT"
                        End With

                        oSuara.InsertData(dsSuara)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
                    End Try
                End If
            Catch ex As Exception
                InsertData = ""
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataFarmasi(ByVal MODUL As String, ByVal entity As R_ORDER_ANTRIANFARMASI) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertDataFarmasi = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
                sSTATUS = "INSERT"

                sMODUL = MODUL

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                sLASTNUMBER = oCounter.GetLastNumberDayAntrianRS(sMODUL, WaktuServer)

                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertDataAntrianRS(sMODUL, WaktuServer)
                        sLASTNUMBER = oCounter.GetLastNumberDayAntrianRS(sMODUL, WaktuServer)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.DATECREATED = WaktuServer
                entity.DATEUPDATED = WaktuServer
                entity.NOMORANTRIAN = WaktuServer.ToString("dd") & (sLASTNUMBER + 1).ToString.PadLeft(3, "0")
                entity.ANGKAANTRIAN = sLASTNUMBER + 1

                Try
                    oCounter.UpdateDataAntrianRS(sMODUL, sLASTNUMBER + 1, Month(WaktuServer), Year(WaktuServer), Day(WaktuServer))
                Catch ex As Exception
                    ''oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_ORDER_ANTRIANFARMASIs.InsertOnSubmit(entity)
                Catch ex As Exception
                    ''oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    ''oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataFarmasi = True
            Catch ex As Exception
                InsertDataFarmasi = False
                ''oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_ORDER) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORDER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = entity.KDORDER)

                Try
                    oConnection.dbRME.R_ORDERs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_ORDERs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True

                If entity.MEMO = "ORDER FARMASI" Then
                    Try
                        Dim oSuara As New Digital.clsSuaraKeFarmasi

                        Dim dsSuara = oSuara.GetStructureHeader
                        With dsSuara
                            .DATECREATED = entity.DATECREATED
                            .DATEUPDATED = entity.DATEUPDATED
                            .KDSUARA = 0
                            If entity.NOMORREFERENCE.Contains("AMIGD") Then
                                .MEMO = "IGD"
                            Else
                                .MEMO = entity.JENISORDER
                            End If
                            .ISCHEKED = True
                            .KDUSER = "UPDATE"
                        End With

                        oSuara.InsertData(dsSuara)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
                    End Try
                End If
            Catch ex As Exception
                UpdateData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDORDER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDORDER
                sSTATUS = "DELETE"


                Dim ds = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)

                Try
                    oConnection.dbRME.R_ORDERs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateStatus(ByVal sKDORDER As String, ByVal sSTATUS As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateStatus = False
                    Exit Function
                End If

                sREFERENCE = sKDORDER

                Try
                    Dim ds = oConnection.dbRME.R_ORDERs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)

                    If ds IsNot Nothing Then
                        ds.STATUS = sSTATUS

                        Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                        Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                        ds.DATEUPDATED = WaktuServer

                        oConnection.dbRME.SubmitChanges()
                    End If

                Catch ex As Exception
                    oError.InsertData("R_ORDER", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateStatus = True
            Catch ex As Exception
                UpdateStatus = False
                oError.InsertData("R_ORDER", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace