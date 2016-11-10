package com.example.admin.pruebatracking.Entidades;

/**
 * Created by Mario on 25/10/2016.
 */
public class Servidor extends DBEntidad{

    public String Nombre;

    public String Ip;

    public String Puerto;

    public Servidor(int Id) {
        super(Id);
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public String getIp() {
        return Ip;
    }

    public void setIp(String ip) {
        Ip = ip;
    }

    public String getPuerto() {
        return Puerto;
    }

    public void setPuerto(String puerto) {
        Puerto = puerto;
    }
}
