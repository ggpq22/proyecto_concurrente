package com.example.admin.pruebatracking.Mensajes;

import com.example.admin.pruebatracking.Entidades.DBEntidad;

import java.util.ArrayList;

public class MsgNotificacion extends Mensaje{

    private MsgDBPeticion Peticion;

    public MsgNotificacion(ArrayList<String> to, String from, String fecha, MsgDBPeticion Peticion) {
        super(to, from, fecha, "MsgNotificacion", true);
        this.Peticion = Peticion;
    }

    public MsgDBPeticion getPeticion() {
        return Peticion;
    }

    public void setPeticion(MsgDBPeticion Peticion) {
        this.Peticion = Peticion;
    }

}
