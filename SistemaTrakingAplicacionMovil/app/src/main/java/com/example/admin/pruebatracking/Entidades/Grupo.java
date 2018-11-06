package com.example.admin.pruebatracking.Entidades;

import java.util.ArrayList;

/**
 * Created by Mario on 25/10/2016.
 */
public class Grupo extends DBEntidad{

    private String Nombre;

    private Cuenta Anfitrion;

    public ArrayList<Cuenta> Integrantes = new ArrayList<>();

    public Grupo(int Id, String Nombre, Cuenta Anfitrion, ArrayList<Cuenta> Integrantes) {
        super(Id);
        this.Nombre = Nombre;
        this.Anfitrion = Anfitrion;
        this.Integrantes = Integrantes;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public Cuenta getAnfitrion() {

        return Anfitrion;
    }

    public void setAnfitrion(Cuenta anfitrion) {

        Anfitrion = anfitrion;
    }

    public ArrayList<Cuenta> getIntegrantes() {
        return Integrantes;
    }

    public void setIntegrantes(ArrayList<Cuenta> integrantes) {
        Integrantes = integrantes;
    }
}
