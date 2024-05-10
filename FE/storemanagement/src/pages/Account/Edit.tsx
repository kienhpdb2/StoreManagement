import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import axios from '../../common/baseAxios';
import EditAccountDto from './models/EditAccountDto';
import AccountDto from './models/AccountDto';
import { GenderEnum } from './models/GenderEnum';
import dayjs from 'dayjs';
interface IOpenModalProps {
    open: boolean,
    closeForm: Function,
    account: AccountDto
}

const EditModal: React.FC<IOpenModalProps> = (props: IOpenModalProps) => {
    const { open, closeForm, account } = props;
    const onFinish = (input: EditAccountDto) => {
        axios.put(`Account/${account!.id}`, input).then((res) => {
            let { data } = res;
            if (data && data.success === true) {
                message.success('Cập nhật tài khoản thành công.')
                closeForm(true);
            }
            else {
                message.error(data.error)
            }
        })
    }
    return <>
        <Modal
            title="CẬP NHẬT TÀI KHOẢN"
            centered
            open={open}
            onOk={() => closeForm(false)}
            onCancel={() => closeForm(false)}
            footer={null}
            width={1000}
        >
            <Form
                name="basic"
                layout={'vertical'}
                style={{ maxWidth: 1000 }}
                onFinish={onFinish}
                autoComplete="off"
                initialValues={{
                    ["fullName"]: account!.fullName,
                    ["phoneNumber"]: account!.phoneNumber,
                    ["email"]: account!.email,
                    ["address"]: account!.address,
                    ["gender"]: account!.gender,
                    ["dob"]: account!.dob && dayjs(account!.dob)
                }}
            >
                <Form.Item label="Tên nhân viên" name="fullName" rules={[{ required: true, message: 'Vui lòng nhập tên nhân viên!' }]}>
                    <Input placeholder='Tên nhân viên' />
                </Form.Item>
                <Form.Item name="phoneNumber" label="Số điện thoại" rules={[{ required: true, message: 'Vui lòng nhập số điện thoại !' }]}>
                    <Input placeholder='Số điện thoại' />
                </Form.Item>
                <Form.Item label="Email" name="email" rules={[
                    { required: true, message: 'Vui lòng nhập email của bạn!' },
                    { type: 'email', message: 'Định dạng E-mail không đúng!' },
                ]}>
                    <Input disabled placeholder='Email của bạn' />
                </Form.Item>
                <Form.Item name="gender" label="Giới Tính" rules={[{ required: true, message: 'Vui lòng chọn giới tính !' }]}>
                    <Select
                        placeholder="Chọn giới tính"
                        allowClear
                    >
                        <Select.Option value={GenderEnum.Male}>Nam</Select.Option>
                        <Select.Option value={GenderEnum.Female}>Nữ</Select.Option>
                        <Select.Option value={GenderEnum.Unknown}>Khác</Select.Option>
                    </Select>
                </Form.Item>
                <Form.Item
                    label="Ngày sinh"
                    name="dob"
                    rules={[
                        { required: true, message: 'Vui lòng nhập ngày sinh của nhân viên!' },
                    ]}
                >
                    <DatePicker style={{ width: '100%' }} placeholder='Ngày sinh của nhân viên' />
                </Form.Item>
                <Form.Item
                    label="Địa chỉ"
                    name="address"
                    rules={[
                        { required: true, message: 'Vui lòng nhập địa chỉ!' },
                    ]}
                >
                    <Input placeholder='Địa chỉ' />
                </Form.Item>
                <Form.Item wrapperCol={{ span: 24 }} style={{ textAlign: 'right' }}>
                    <Button type="primary" htmlType="submit" >
                        Lưu
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    </>
}

export default EditModal;