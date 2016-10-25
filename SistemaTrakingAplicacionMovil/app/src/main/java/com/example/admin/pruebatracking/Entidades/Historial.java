package com.example.admin.pruebatracking.Entidades;

import java.util.Date;

/**
 * Created by Mario on 25/10/2016.
 */
public class Historial {

    private int IdHistorial;

    private int IdGrupo;

    private int IdCuenta;

    private Date Fecha;

    private float Latitud;

    private float Longitud;

    public Historial() {
    }

    public int getIdHistorial() {
        return IdHistorial;
    }

    public void setIdHistorial(int idHistorial) {
        this.IdHistorial = idHistorial;
    }

    public int getIdGrupo() {
        return IdGrupo;
    }

    public void setIdGrupo(int idGrupo) {
        this.IdGrupo = idGrupo;
    }

    public int getIdCuenta() {
        return IdCuenta;
    }

    public void setIdCuenta(int idCuenta) {
        this.IdCuenta = idCuenta;
    }

    public Date getFecha() {
        return Fecha;
    }

    public void setFecha(Date fecha) {
        this.Fecha = fecha;
    }

    public float getLatitud() {
        return Latitud;
    }

    public void setLatitud(float latitud) {
        this.Latitud = latitud;
    }

    public float getLongitud() {
        return Longitud;
    }

    public void setLongitud(float longitud) {
        this.Longitud = longitud;
    }

}
