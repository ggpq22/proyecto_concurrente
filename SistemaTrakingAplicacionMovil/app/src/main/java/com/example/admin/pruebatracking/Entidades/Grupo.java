package com.example.admin.pruebatracking.Entidades;

import java.util.ArrayList;

/**
 * Created by Mario on 25/10/2016.
 */
public class Grupo extends DBEntidad{

    private Cuenta Anfitrion;

    public ArrayList<Cuenta> Integrantes = new ArrayList<>();

    public Grupo(int Id) {
        super(Id);
    }

    public Cuenta getAnfitrion() {
        return Anfitrion;
    }

    public void setAnfitrion(Cuenta anfitrion) {
        Anfitrion = anfitrion;
    }
}
