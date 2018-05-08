import { DataSource } from '@angular/cdk/collections';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { TimeSpentTableElement } from './timespent-table-element';
import { ValidatorService } from './validator.service';
import { DefaultValidatorService } from './default-validator.service';
import { TimeSpentService, TimeSpent, CreateTimeSpent } from '../timespent.service';
import { LoginService, AuthenticatedUser } from '../login.service';

export class TimeSpentTableDataSource extends DataSource<TimeSpentTableElement> {

  private rowsSubject: BehaviorSubject<TimeSpentTableElement[]>;
  datasourceSubject: Subject<TimeSpent>;

  private dataConstructor: new () => TimeSpent;
  private dataKeys: any[];

  private currentData: any;

  /**
   * Creates a new TableDataSource instance, that can be used as datasource of `@angular/cdk` data-table.
   * @param data Array containing the initial values for the TableDataSource. If not specified, then `dataType` must be specified.
   * @param dataType Type of data contained by the Table. If not specified, then `data` with at least one element must be specified.
   * @param validatorService Service that create instances of the FormGroup used to validate row fields.
   * @param config Additional configuration for table.
   */
  constructor(
    data: TimeSpent[],
    private timeSpentService: TimeSpentService,
    private login: LoginService,
    private problemId: number,
    dataType?: new () => TimeSpent,
    private validatorService?: ValidatorService,
    private config = { prependNewElements: false })
  {
    super();

    if (!validatorService)
      this.validatorService = new DefaultValidatorService();

    if (dataType) {
      this.dataConstructor = dataType;
    } else {
      if (data && data.length > 0)
        this.dataKeys = Object.keys(data[0]);
      else
        throw new Error('You must define either a non empty array, or an associated class to build the table.');
    }

    this.rowsSubject = new BehaviorSubject(this.getRowsFromData(data));
    this.datasourceSubject = new Subject<TimeSpent>();
  }

  /**
   * Start the creation of a new element, pushing an empty-data row in the table.
   */
  createNew(): void {
    const source = this.rowsSubject.getValue();

    if (!this.existsNewElement(source)) {

      const newElement = new TimeSpentTableElement({
        id: -1,
        editing: true,
        currentData: this.createNewObject(),
        source: this,
        validator: this.validatorService!.getRowValidator(),
      });

      if (this.config.prependNewElements) {
        this.rowsSubject.next([newElement].concat(source));
      } else {
        source.push(newElement);
        this.rowsSubject.next(source);
      }
    }
  }

  /**
   * Confirm creation of the row. Save changes and disable editing.
   * If validation active and row data is invalid, it doesn't confirm creation neither disable editing.
   * @param row Row to be confirmed.
   */
  confirmCreate(row: TimeSpentTableElement): boolean {
    if (!row.validator.valid) {
      return false
    }

    const source = this.rowsSubject.getValue();
    row.id = source.length - 1;
    this.rowsSubject.next(source);
    row.editing = false;
    row.validator.disable();
    var userId = this.login.getAuthenticatedUser().id;
    var timeSpent = row.currentData;
    timeSpent.userId = userId;
    timeSpent.problemId = this.problemId; 
    this.timeSpentService.createTimeSpent(timeSpent).subscribe(data => {
      row.currentData.id = data.id;
      console.log(row.currentData);
    });

    return true;
  }

  /**
   * Confirm edition of the row. Save changes and disable editing.
   * If validation active and row data is invalid, it doesn't confirm editing neither disable editing.
   * @param row Row to be edited.
   */
  confirmEdit(row: TimeSpentTableElement): boolean {
    if (!row.validator.valid) {
      return false;
    }

    var timeSpent = row.currentData; 
    this.timeSpentService.update(timeSpent, timeSpent.id).subscribe(res => {
      const source = this.rowsSubject.getValue();
      const index = this.getIndexFromRowId(row.id, source);
      source[index] = row;
      this.rowsSubject.next(source);
      row.editing = false;
      row.validator.disable();
      return true;
    },
    err => {
      return false;
    });
    return false;
  }

  /**
   * Delete the row with the index specified.
   */
  delete(id: number): void {
    const source = this.rowsSubject.getValue();
    const index = this.getIndexFromRowId(id, source);
    var timeSpent = source[index].currentData;

    this.timeSpentService.delete(timeSpent.id,).subscribe(res => {
      source.splice(index, 1);
      this.updateRowIds(index, source);
      this.rowsSubject.next(source);
    },
    err => {
      console.log(err);
    });  
  }

  /**
 * Get row from the table.
 * @param id Id of the row to retrieve, -1 returns the current new line.
 */
  getRow(id: number): TimeSpentTableElement {
    const source = this.rowsSubject.getValue();
    const index = this.getIndexFromRowId(id, source);

    return (index >= 0 && index < source.length) ? source[index] : null!;
  }


  /**
   * Checks the existance of the a new row (not yet saved).
   * @param source 
   */
  private existsNewElement(source: TimeSpentTableElement[]): boolean {
      return !(source.length == 0 || source[this.getNewRowIndex(source)].id > -1)
  }

  /**
   * Returns the possible index of the new row depending on the insertion type.
   * It doesn't imply that the new row is created, that must be checked.
   * @param source 
   */
  private getNewRowIndex(source: any): number {
    if (this.config.prependNewElements)
      return 0;
    else
      return source.length - 1;
  }

  /**
   * Returns the row id from the index specified. It does
   * not consider if the new row is present or not, assumes
   * that new row is not present.
   * @param index Index of the array.
   * @param count Quantity of elements in the array.
   */
  private getRowIdFromIndex(index: number, count: number): number {
    if (this.config.prependNewElements)
      return count - 1 - index;
    else
      return index;
  }

  /**
   * Returns the index from the row id specified.
   * It takes into account if the new row exists or not.
   * @param id 
   * @param source
   */
  private getIndexFromRowId(id: number, source: TimeSpentTableElement[]): number {
    if(id == -1) {
      return this.existsNewElement(source) ? this.getNewRowIndex(source) : -1;
    } else {
      if (this.config.prependNewElements)
          return source.length - 1 - id;
      else
        return id;
    }
  }

  /**
   * Update rows ids in the array specified, starting in the specified index
   * until the start/end of the array, depending on config.prependNewElements
   * configuration.
   * @param initialIndex Initial index of source to be updated.
   * @param source Array that contains the rows to be updated.
   */
  private updateRowIds(initialIndex: number, source: TimeSpentTableElement[]): void {

    const delta = this.config.prependNewElements ? -1 : 1;

    for (let index = initialIndex; index < source.length && index >= 0; index += delta) {
      if (source[index].id != -1)
        source[index].id = this.getRowIdFromIndex(index, source.length);
    }
  }

  /**
   * Get the data from the rows.
   * @param rows Rows to extract the data.
   */
  private getDataFromRows(rows: TimeSpentTableElement[]): TimeSpent[] {
    return rows
      .filter(row => row.id != -1)
      .map<TimeSpent>((row: any) => {
      return row.originalData ? row.originalData : row.currentData;
    });
  }


  /**
   * From an array of data, it returns rows containing the original data.
   * @param arrayData Data from which create the rows.
   */
  private getRowsFromData(arrayData: TimeSpent[]): TimeSpentTableElement[] {
    return arrayData.map<TimeSpentTableElement>((data, index) => {

      const validator = this.validatorService!.getRowValidator();
      validator.disable();

      return new TimeSpentTableElement({
        id: this.getRowIdFromIndex(index, arrayData.length),
        editing: false,
        currentData: data,
        source: this,
        validator: validator,
      })
    });
  }

  /**
   * Create a new object with identical structure than the table source data.
   * It uses the object's type contructor if available, otherwise it creates
   * an object with the same keys of the first element contained in the original
   * datasource (used in the constructor).
   */
  private createNewObject(): TimeSpent {
    if (this.dataConstructor)
      return new this.dataConstructor();
    else {
      return this.dataKeys.reduce((obj, key) => {
        obj[key] = undefined;
        return obj;
      }, {});
    }

  }

  /** Connect function called by the table to retrieve one stream containing
   *  the data to render. */
  connect(): Observable<TimeSpentTableElement[]> {
    return this.rowsSubject.asObservable();
  }

  disconnect() { }
}
