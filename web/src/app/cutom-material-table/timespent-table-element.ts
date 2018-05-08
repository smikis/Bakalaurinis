import { FormGroup } from '@angular/forms';
import { TimeSpentTableDataSource } from './timespent-table-data-source';
import { cloneDeep } from 'lodash';
import { TimeSpent } from '../timespent.service';
export class TimeSpentTableElement {
  id: number;
  editing: boolean;
  currentData: TimeSpent;
  originalData: TimeSpent;
  source: TimeSpentTableDataSource;
  validator: FormGroup;

  constructor(init: any) {
    Object.assign(this, init);
  }

  delete(): void {
    this.source.delete(this.id);
  }

  confirmEditCreate(): boolean {
    this.originalData = undefined!;
    if (this.id == -1)
      return this.source.confirmCreate(this);
    else
      return this.source.confirmEdit(this);
  }

  startEdit(): void {
    this.originalData = cloneDeep(this.currentData)!;
    this.editing = true;
    this.validator.enable();
  }

  cancelOrDelete(): void {
    if (this.id == -1 || !this.editing)
      this.delete();
    else {
      this.currentData = this.originalData;
      this.editing = false;
      this.validator.disable();
    }
  }
}