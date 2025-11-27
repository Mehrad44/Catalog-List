import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-confirm-delete',
  standalone: true,
  imports:[AppModule],
  templateUrl: './confirm-delete.component.html',
  styleUrl: './confirm-delete.component.scss'
})
export class ConfirmDeleteComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public confirmMessage: string,
    public dialogRef: MatDialogRef<ConfirmDeleteComponent>,
  ) { }

  // delete() {
  //   this.dialogRef.close(false);
  // }

  // cancel() {
  //   this.dialogRef.close(false);
  // }
}
