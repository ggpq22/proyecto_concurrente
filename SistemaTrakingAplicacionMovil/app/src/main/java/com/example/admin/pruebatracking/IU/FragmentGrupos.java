package com.example.admin.pruebatracking.IU;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;


import com.example.admin.pruebatracking.AplicacionPrincipal;
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;


public class FragmentGrupos extends Fragment {

    ListViewAdapterGrupos adapter;
    ArrayList<Grupo> grupos;


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View rootView = inflater.inflate(R.layout.fragment_grupos, container, false);

        ListView lista = (ListView) rootView.findViewById(R.id.listView_listarGrupos);


        grupos = ((AplicacionPrincipal)getContext().getApplicationContext()).getGrupos();
        adapter = new ListViewAdapterGrupos(getContext(), grupos);
        lista.setAdapter(adapter);
        ((AplicacionPrincipal)getContext().getApplicationContext()).setAdapterGrupos(adapter);


        lista.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent i = new Intent(getActivity(), ListarUnGrupo.class);
                i.putExtra("position", position);
                startActivity(i);

            }
        });
        return rootView;
    }
}
