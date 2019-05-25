﻿
Imports System.Data.OleDb
Imports Penjualan

Public Class ClsCtlBarang : Implements InfProses

    Function kodeBaru() As String
        Dim baru As String
        Dim kodeakhir As Integer
        Try
            DTA = New OleDbDataAdapter("select max(right(kode_barang,4)) from barang", BUKAKONEKSI)
            DTS = New DataSet()
            DTA.Fill(DTS, "max_kode")
            kodeakhir = Val(DTS.Tables("max_kode").Rows(0).Item(0))
            baru = "B" & Strings.Right("000" & kodeakhir + 1, 4)
            Return baru
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function cariData(kunci As String) As DataView Implements InfProses.cariData
        Try
            DTA = New OleDbDataAdapter("select * from barang where nama_barang " _
                                       & "like '%" & kunci & "%'", BUKAKONEKSI)
            DTS = New DataSet()
            DTA.Fill(DTS, "cari_barang")
            Dim grid As New DataView(DTS.Tables("cari_barang"))
            Return grid
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function deleteData(kunci As String) As OleDb.OleDbCommand Implements InfProses.deleteData
        CMD = New OleDbCommand("delete from barang " _
                                & "where kode_barang='" & kunci & "'", BUKAKONEKSI)
        CMD.CommandType = CommandType.Text
        CMD.ExecuteNonQuery()
        CMD = New OleDbCommand("", TUTUPKONEKSI)
        Return CMD
    End Function

    Public Function InsertData(Ob As Object) As OleDb.OleDbCommand Implements InfProses.InsertData
        Dim data As New ClsEntBarang
        data = Ob
        CMD = New OleDbCommand("insert into barang values('" & data.kodeBarang & "','" _
                               & data.namaBarang & "'," & data.hargaBarang & "," _
                               & data.stokBarang & ")", BUKAKONEKSI)
        CMD.CommandType = CommandType.Text
        CMD.ExecuteNonQuery()
        CMD = New OleDbCommand("", TUTUPKONEKSI)
        Return CMD
    End Function

    Public Function tampilData() As DataView Implements InfProses.tampilData
        Try
            DTA = New OleDbDataAdapter("select * from Barang", BUKAKONEKSI)

            DTS = New DataSet()
            DTA.Fill(DTS, "Tabel_Barang")
            Dim grid As New DataView(DTS.Tables("Tabel_Barang"))

            Return grid

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function updateData(Ob As Object) As OleDb.OleDbCommand Implements InfProses.updateData
        Dim data As New ClsEntBarang
        data = Ob
        CMD = New OleDbCommand("update barang set nama_barang='" _
                                & data.namaBarang & "',harga_satuan=" & data.hargaBarang _
                                & ",stok=" & data.stokBarang _
                                & "where kode_barang='" & data.kodeBarang & "'", BUKAKONEKSI)
        CMD.CommandType = CommandType.Text
        CMD.ExecuteNonQuery()
        CMD = New OleDbCommand("", TUTUPKONEKSI)
        Return CMD
    End Function

    Function cekBarangDireferensi(kunci As String) As Boolean
        Dim cek As Boolean
        cek = False
        Try
            DTA = New OleDbDataAdapter("select count(kd_barang) from detail_nota " _
                                       & "where kd_barang='" & kunci & "'", BUKAKONEKSI)
            DTS = New DataSet()
            DTA.Fill(DTS, "cek")
            If DTS.Tables("cek").Rows(0)(0).ToString > 0 Then cek = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return cek
    End Function

End Class
