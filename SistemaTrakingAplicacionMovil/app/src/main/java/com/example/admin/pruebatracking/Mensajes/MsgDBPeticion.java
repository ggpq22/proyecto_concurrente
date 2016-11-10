package com.example.admin.pruebatracking.Mensajes;

import android.app.Notification;

import com.example.admin.pruebatracking.Entidades.DBEntidad;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Admin on 1/11/2016.
 */
public class MsgDBPeticion extends Mensaje {

    private String CodigoPeticion;
    private ArrayList<DBEntidad> Params;
    private boolean Notificacion;

    public MsgDBPeticion(ArrayList<String> to, String from, String fecha, String CodigoPeticion, ArrayList<DBEntidad> Params, boolean Notificacion) {
        super(to, from, fecha, "MsgDBPeticion", true);
        this.CodigoPeticion = CodigoPeticion;
        this.Params = Params;
        this.Notificacion = Notificacion;
    }

    public String getCodigoPeticion() {
        return CodigoPeticion;
    }

    public void setCodigoPeticion(String CodigoPeticion) {
        this.CodigoPeticion = CodigoPeticion;
    }

}
