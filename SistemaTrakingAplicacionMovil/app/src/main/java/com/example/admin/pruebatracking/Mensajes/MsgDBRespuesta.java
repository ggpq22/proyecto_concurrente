package com.example.admin.pruebatracking.Mensajes;

import com.example.admin.pruebatracking.Entidades.DBEntidad;

import java.util.ArrayList;

/**
 * Created by Admin on 1/11/2016.
 */
public class MsgDBRespuesta extends Mensaje {

    private String CodigoPeticion;
    private ArrayList<DBEntidad> Return = new ArrayList<DBEntidad>();

    public MsgDBRespuesta(ArrayList<String> to, String from, String fecha) {
        super(to, from, fecha, "MsgDBRespuesta", true);
    }

    public String getCodigoPeticion() {
        return CodigoPeticion;
    }

    public void setCodigoPeticion(String CodigoPeticion) {
        this.CodigoPeticion = CodigoPeticion;
    }
}
