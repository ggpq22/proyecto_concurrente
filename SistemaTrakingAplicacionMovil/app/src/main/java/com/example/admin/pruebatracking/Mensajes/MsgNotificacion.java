package com.example.admin.pruebatracking.Mensajes;

import com.example.admin.pruebatracking.Entidades.DBEntidad;

import java.util.ArrayList;

public class MsgNotificacion extends Mensaje{

    private MsgDBRespuesta Respuesta;

    public MsgNotificacion(ArrayList<String> to, String from, String fecha, MsgDBPeticion Peticion) {
        super(to, from, fecha, "MsgNotificacion", true);
        this.Respuesta = Respuesta;
    }

    public MsgDBRespuesta getRespuesta() {
        return Respuesta;
    }

    public void setRespuesta(MsgDBRespuesta Respuesta) {
        this.Respuesta = Respuesta;
    }

}
