Objeto "Cutter":
- Este objeto contiene un script que corta un objeto con el tag "Cuttable" usando la mesh que tenga asignada.
- La parte cortada tendrá la textura del objeto "Cutter".
- Es muy importante que ambos objetos esten con los tres valores de escala en 1 y que el pivote esté en el centro del objeto.
  - La razón para esto es que usa las meshes como tal en vez de los GameObjects para crear el corte. 